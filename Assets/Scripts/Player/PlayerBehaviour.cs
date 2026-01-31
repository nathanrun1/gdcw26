using System;
using Managers;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities.Types;

namespace Player
{
    public class PlayerBehaviour : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private SpriteRenderer _maskSpriteRenderer;
        [Header("Config")] [SerializeField] private float _walkSpeed;

        private void Start()
        {
            MaskManager.Instance.onMaskChange += OnMaskChange;
            OnMaskChange(MaskManager.Instance.GetMaskColor());
        }

        private void MovePlayer()
        {
            var rawInput = InputManager.Instance.playerInput.Player.Move.ReadValue<Vector2>();
            Vector2 movementVector = rawInput.magnitude > 0.95f ? rawInput : Vector2.zero;
            _rigidbody2D.MovePosition(_rigidbody2D.position + movementVector * (_walkSpeed * Time.fixedDeltaTime));
        }

        private void FixedUpdate()
        {
            MovePlayer();
        }

        private void OnMaskChange(MaskColor newColor)
        {
            _maskSpriteRenderer.enabled = newColor != MaskColor.Default;
            _maskSpriteRenderer.color = newColor.GetColor();
        }
    }
}