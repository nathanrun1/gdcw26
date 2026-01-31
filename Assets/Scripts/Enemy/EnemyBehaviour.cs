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
    public class EnemyBehaviour : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private Collider2D _collider2D;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        [Header("Config")]
        [SerializeField] private MaskColor _maskColor;
        [SerializeField] private MonitorDirection _monitorDirection;
        [SerializeField] private DetectionMode _detectionMode = DetectionMode.Cone;
        
        [Header("Cone Detection")]
        [SerializeField] private float _monitorFovDegrees;

        [Header("Rectangular Detection")] 
        [SerializeField] private float _detectionWidth = 2f;
        [SerializeField] private float _detectionDistance = 20f;
        
        [Header("Movement")]
        [SerializeField] private float _walkSpeed = 10;  // Movement speed in units/s
        [SerializeField] private float _turnLimit = -1f; // Max turning speed in degrees/s
        
        private bool _isEngaged = true;
        private Vector2 _curMovementDirection;

        private void OnValidate()
        {
            _spriteRenderer.color = _maskColor.GetColor();
            _curMovementDirection = _monitorDirection.ToVector2();
        }

        private void Start()
        {
            MaskManager.Instance.onColorChange += OnMaskUpdate;
            SetEngaged(MaskManager.Instance.GetMaskColor() != _maskColor);
        }

        private void FixedUpdate()
        {
            if (TryDetectPlayer())
            {
                TurnTowardPlayer();
                Move();
            }
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject != LevelManager.Instance.playerObject) return;
            GameManager.PlayerLoss();
        }
        

        /// <summary>
        /// Invoked when global mask color is updated
        /// </summary>
        private void OnMaskUpdate(MaskColor newColor)
        {
            SetEngaged(_maskColor != newColor);
        }

        /// <summary>
        /// Sets whether the enemy is "engaged". That is, whether hitbox, sprite, and aggro is enabled.
        /// </summary>
        /// <param name="engaged"></param>
        private void SetEngaged(bool engaged)
        {
            _spriteRenderer.enabled = engaged;
            _collider2D.enabled = engaged;
            _isEngaged = engaged;
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
            Debug.Log($"Angle diff: {deltaAngle}");
            float frameTurnLimit = _turnLimit * Time.fixedDeltaTime;
            Debug.Log($"Turn limit: {frameTurnLimit}");
            if (frameTurnLimit * Mathf.Deg2Rad < Mathf.Abs(deltaAngle)) deltaAngle = Mathf.Sign(deltaAngle) * frameTurnLimit;
        
            Debug.Log($"Turning by {deltaAngle}");
            _curMovementDirection = _curMovementDirection.Rotate(deltaAngle);
        }
    }
}

// Have some desired direction, and some current direction
// Rotate by minimum of angle to desired direction and turn limit * deltatime