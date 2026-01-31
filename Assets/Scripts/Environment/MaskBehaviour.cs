using Managers;
using UnityEngine;
using Utilities.Types;

namespace Environment
{
    public class MaskBehaviour : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [Header("Config")]
        [SerializeField] private MaskColor _maskColor;

        private void OnValidate()
        {
            _spriteRenderer.color = Color.Lerp(_maskColor.GetColor(), Color.white,0.5f);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            MaskManager.Instance.QueueMaskColor(_maskColor);
            Destroy(gameObject);
        }
    }
}