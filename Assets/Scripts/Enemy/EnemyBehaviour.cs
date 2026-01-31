using System;
using Managers;
using UnityEngine;
using Utilities;
using Utilities.Types;

namespace Enemy
{
    public class EnemyBehaviour : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private Collider2D _collider2D;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [Header("Config")]
        [SerializeField] private MaskColor _maskColor;
        [SerializeField] private MonitorDirection _monitorDirection;
        [SerializeField] private float _monitorFovDegrees;
        [SerializeField] private float _walkSpeed = 10;

        private bool _isEngaged = true;

        private void Start()
        {
            _spriteRenderer.color = _maskColor.GetColor();
            MaskManager.Instance.onColorChange += OnMaskUpdate;
            SetEngaged(MaskManager.Instance.GetMaskColor() != _maskColor);
        }

        private void FixedUpdate()
        {
            if (TryDetectPlayer())
            {
                MoveToPlayer();
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
            Vector2 playerDir = LevelManager.Instance.playerPosition - (Vector2)transform.position;
            
            float angleToPlayer =
                Mathf.Acos(Mathf.Clamp01(Vector2.Dot(playerDir.normalized, _monitorDirection.ToVector2()))) * Mathf.Rad2Deg;
            if (angleToPlayer > _monitorFovDegrees / 2f) return false;  // Not within FOV
            Debug.Log($" {angleToPlayer} within angle");
            RaycastHit2D hit = Physics2D.Raycast(transform.position, playerDir.normalized, playerDir.magnitude * 2f,
                (int)CollisionAssistant.VisibleToEnemy);
            return hit && hit.collider.gameObject == LevelManager.Instance.playerObject;
        }

        private void MoveToPlayer()
        {
            Vector2 playerDir = LevelManager.Instance.playerPosition - (Vector2)transform.position;
            _rigidbody2D.MovePosition(_rigidbody2D.position + playerDir.normalized * (_walkSpeed * Time.fixedDeltaTime));
        }
    }
}