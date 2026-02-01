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
        [SerializeField] protected Rigidbody2D _rigidbody2D;
        [SerializeField] protected Collider2D _collider2D;
        [SerializeField] protected SpriteRenderer _spriteRenderer;
        [SerializeField] protected SpriteRenderer[] _subSpriteRenderers;
        
        [Header("Config")]
        [SerializeField] protected MaskColor _maskColor;
        [SerializeField] protected MonitorDirection _monitorDirection;
        
        protected bool _isEngaged = true;
        

        protected virtual void OnValidate()
        {
            _spriteRenderer.color = _maskColor.GetColor();
        }

        private void Start()
        {
            MaskManager.Instance.onMaskChange += OnMaskUpdate;
            SetEngaged(MaskManager.Instance.GetMaskColor() != _maskColor);
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
            foreach (SpriteRenderer sr in _subSpriteRenderers)
            {
                sr.enabled = engaged;
            }
        }
    }
}

// Have some desired direction, and some current direction
// Rotate by minimum of angle to desired direction and turn limit * deltatime