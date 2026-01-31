using System;
using Managers;
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

        private void Start()
        {
            _spriteRenderer.color = _maskColor.GetRGB();
            MaskManager.Instance.onColorChange += OnMaskUpdate;
        }

        /// <summary>
        /// Invoked when global mask color is updated
        /// </summary>
        private void OnMaskUpdate(MaskColor newColor)
        {
            SetEngaged(_maskColor != newColor);
        }

        private void SetEngaged(bool engaged)
        {
            _spriteRenderer.enabled = engaged;
            _collider2D.enabled = engaged;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("Player collision!");
        }
    }
}