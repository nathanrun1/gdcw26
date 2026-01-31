using System;
using UnityEngine;
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

        private void Awake()
        {
            _spriteRenderer.color = _maskColor.GetRGB();
        }

        /// <summary>
        /// Invoked when global mask color is updated
        /// </summary>
        private void OnMaskUpdate(MaskColor newColor)
        {
            if (_maskColor == newColor) return;
        }

        private void SetEngaged(bool engaged)
        {
            _spriteRenderer.enabled = engaged;
            _collider2D.enabled = engaged;
        }
    }
}