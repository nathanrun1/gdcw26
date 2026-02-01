using System;
using Managers;
using UnityEngine;
using Utilities;
using Utilities.Types;

namespace Enemy
{
    public enum DetectionMode
    {
        Cone,
        Rectangular
    }
    public class ChargerBehaviour : EnemyBehaviour
    {  
        [Header("Charger Config")]
        [SerializeField] private DetectionMode _detectionMode;
        
        [Header("Cone Detection")]
        [SerializeField] private float _monitorFovDegrees;

        [Header("Rectangular Detection")] 
        [SerializeField] private float _detectionWidth = 2f;
        [SerializeField] private float _detectionDistance = 20f;
        
        [Header("Movement")]
        [SerializeField] private float _walkSpeed = 10;  // Movement speed in units/s
        [SerializeField] private float _turnLimit = -1f; // Max turning speed in degrees/s
        
        private Vector2 _curMovementDirection;

        protected override void OnValidate()
        {
            base.OnValidate();
            _curMovementDirection = _monitorDirection.ToVector2();
            AlignWithDirection();
        }
        
        private void Update()
        {
            AlignWithDirection();
        }
        
        private void FixedUpdate()
        {
            if (!TryDetectPlayer()) return;
            TurnTowardPlayer();
            Move();
        }
        
        /// <summary>
        /// Attempts to detect the player. Will successfully detect if the player is within FOV and not behind
        /// an obstacle.
        /// </summary>
        private bool TryDetectPlayer()
        {
            if (!_isEngaged) return false;

            return _detectionMode switch
            {
                DetectionMode.Cone => TryDetectPlayerCone(),
                DetectionMode.Rectangular => TryDetectPlayerRectangular(),
                _ => false
            };
        }

        private bool TryDetectPlayerCone()
        {
            Vector2 playerDir = LevelManager.Instance.playerPosition - (Vector2)transform.position;
            
            float angleToPlayer =
                Mathf.Acos(Mathf.Clamp01(Vector2.Dot(playerDir.normalized, _monitorDirection.ToVector2()))) * Mathf.Rad2Deg;
            if (angleToPlayer > _monitorFovDegrees / 2f) return false;  // Not within FOV
            RaycastHit2D hit = Physics2D.Raycast(transform.position, playerDir.normalized, playerDir.magnitude * 2f,
                (int)CollisionAssistant.VisibleToEnemy);
            return hit && hit.collider.gameObject == LevelManager.Instance.playerObject;
        }
        
        private bool TryDetectPlayerRectangular()
        {
            Vector2 playerPos =  LevelManager.Instance.playerPosition;
            Vector2 enemyPos = transform.position;
            Vector2 toPlayer = playerPos - enemyPos;
            Vector2 forward = _curMovementDirection;
            
            Vector2 right = new Vector2(forward.y, -forward.x);
            
            float forwardDist = Vector2.Dot(forward, toPlayer);
            float lateralDist = Mathf.Abs(Vector2.Dot(right, toPlayer));
            
            if (forwardDist < 0) return false; // player behind
            if (forwardDist > _detectionDistance) return false; // player outside range
            if (lateralDist > _detectionWidth / 2f) return false; // player not in width

            RaycastHit2D hit = Physics2D.Raycast(enemyPos, toPlayer.normalized, toPlayer.magnitude,
                (int)CollisionAssistant.VisibleToEnemy);
            
            return hit && hit.collider.gameObject == LevelManager.Instance.playerObject;
        }
        
        private void Move()
        {
            _rigidbody2D.MovePosition(_rigidbody2D.position + _curMovementDirection * (_walkSpeed * Time.fixedDeltaTime));
        }
        
        /// <summary>
        /// Changes the current movement direction as much as possible toward the player direction, based on
        /// turning speed limit
        /// </summary>
        private void TurnTowardPlayer()
        {
            Vector2 playerDir = (LevelManager.Instance.playerPosition - (Vector2)transform.position).normalized;
            if (_turnLimit < 0f)
            {
                // No turn limit, set movement direction directly to player direction
                _curMovementDirection = playerDir;
                return;
            }

            float angleFrom = Mathf.Atan2(_curMovementDirection.y, _curMovementDirection.x);
            float angleTo = Mathf.Atan2(playerDir.y, playerDir.x);
            float deltaAngle = Mathf.DeltaAngle(angleFrom, angleTo);
            float frameTurnLimit = _turnLimit * Time.fixedDeltaTime;
            if (frameTurnLimit * Mathf.Deg2Rad < Mathf.Abs(deltaAngle)) deltaAngle = Mathf.Sign(deltaAngle) * frameTurnLimit;
        
            _curMovementDirection = _curMovementDirection.Rotate(deltaAngle);
        }
        
        /// <summary>
        /// Aligns the enemy's rotation (visually) with the direction they're moving in/monitoring
        /// </summary>
        private void AlignWithDirection()
        {
            Vector3 curRot = transform.eulerAngles;
            curRot.z = Mathf.Atan2(_curMovementDirection.y, _curMovementDirection.x) * Mathf.Rad2Deg + 90f;
            transform.eulerAngles = curRot;
        }
    }
}

// Have some desired direction, and some current direction
// Rotate by minimum of angle to desired direction and turn limit * deltatime