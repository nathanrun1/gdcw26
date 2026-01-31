using Managers;
using UnityEngine;

namespace Player
{
     public class PlayerBehaviour : MonoBehaviour
     {
          [SerializeField] private Rigidbody2D _rigidbody2D;
          [Header("Config")]
          [SerializeField] private float _walkSpeed;

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
     }
}
