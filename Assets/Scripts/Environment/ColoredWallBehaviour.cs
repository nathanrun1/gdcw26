using Managers;
using UnityEngine;
using Utilities.Types;

namespace Environment
{
    public class ColoredWallBehaviour : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Collider2D _collider2D;
        [Header("Config")]
        [SerializeField] private MaskColor _maskColor;

        private void Start()
        {
            MaskManager.Instance.onColorChange += (newMaskColor) => SetEngaged(newMaskColor != _maskColor);
            SetEngaged(MaskManager.Instance.GetMaskColor() != _maskColor);
        }
        
        private void OnValidate()
        {
            _spriteRenderer.color = Color.Lerp(_maskColor.GetColor(), Color.gray,0.7f);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            MaskManager.Instance.QueueMaskColor(_maskColor);
            Destroy(gameObject);
        }
        
        /// <summary>
        /// Sets whether the wall is "engaged". That is, whether hitbox and sprite are enabled
        /// </summary>
        /// <param name="engaged"></param>
        private void SetEngaged(bool engaged)
        {
            _spriteRenderer.enabled = engaged;
            _collider2D.enabled = engaged;
        }
    }
}