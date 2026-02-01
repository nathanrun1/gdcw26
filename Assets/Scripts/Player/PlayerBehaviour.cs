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
        [Header("Config")] 
        [SerializeField] private float _walkSpeed;
        [SerializeField] private float _turnSpeedLimit = 1f;
        [SerializeField] private float _turnThreshold = 5f;

        private Vector2 _movementVector = Vector2.right;

        private void Start()
        {
            MaskManager.Instance.onMaskChange += OnMaskChange;
            OnMaskChange(MaskManager.Instance.GetMaskColor());
        }

        private void UpdateMovementVector()
        {
            var rawInput = InputManager.Instance.playerInput.Player.Move.ReadValue<Vector2>();
            _movementVector = rawInput.magnitude > 0.95f ? rawInput : Vector2.zero;
        }

        private void MovePlayer()
        {
            _rigidbody2D.MovePosition(_rigidbody2D.position + _movementVector * (_walkSpeed * Time.fixedDeltaTime));
        }

        private void Update()
        {
            UpdateMovementVector();
            AlignTowardMovement(Time.deltaTime);
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

        /// <summary>
        /// Aligns the player's rotation toward the direction its moving, with alignment speed capped
        /// by '_turnSpeedLimit'
        /// </summary>
        private void AlignTowardMovement(float deltaTime)
        {
            if (_movementVector.magnitude < 0.5f) return;
            
            Vector3 curRot = transform.eulerAngles;
            curRot.z -= 90f;  // Sprite rotation offset
            
            float angleFrom = curRot.z;
            float angleTo = Mathf.Atan2(_movementVector.y, _movementVector.x) * Mathf.Rad2Deg;
            float deltaAngle = Mathf.DeltaAngle(angleFrom, angleTo);
            if (Mathf.Abs(deltaAngle) < _turnThreshold) return;
            float frameTurnLimit = _turnSpeedLimit * deltaTime;
            if (frameTurnLimit < Mathf.Abs(deltaAngle)) deltaAngle = Mathf.Sign(deltaAngle) * frameTurnLimit;

            curRot.z += deltaAngle;
            curRot.z += 90f;  // Undo sprite rotation offset
            transform.eulerAngles = curRot;
        }
    }
}