using Managers;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities.Types;

namespace Player
{
     public class PlayerBehaviour : MonoBehaviour
     {
          [SerializeField] private Rigidbody2D _rigidbody2D;
          [Header("Config")]
          [SerializeField] private float _walkSpeed;

        private void Start()
        {
            InputManager.Instance.playerInput.Player.NumberKey.performed += OnNumberKey;
            InputManager.Instance.playerInput.Player.DefaultMask.performed += OnInputDefaultMask;
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

        private void OnInputDefaultMask(InputAction.CallbackContext _)
        {
            MaskManager.Instance.ChangeMaskColor(MaskColor.Default);
        }

        private void OnNumberKey(InputAction.CallbackContext ctx)
          {
               int number = (int)ctx.ReadValue<float>();
               switch (number)
               {
                    case 1:
                         MaskManager.Instance.ChangeMaskColor(MaskColor.Red);
                         break;
                    case 2:
                         MaskManager.Instance.ChangeMaskColor(MaskColor.Blue);
                         break;
                    case 3:
                         MaskManager.Instance.ChangeMaskColor(MaskColor.Default);
                         break;
               }
          }
     }
}
