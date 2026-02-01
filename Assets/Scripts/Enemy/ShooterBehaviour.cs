using System;
using Managers;
using UnityEngine;
using Utilities;
using Utilities.Types;

namespace Enemy
{
    public class ShooterBehaviour : EnemyBehaviour
    {  
        private Vector2 _curMovementDirection;

        protected override void OnValidate()
        {
            base.OnValidate();
            AlignWithMonitorDirection();
        }

        private void FixedUpdate()
        {
            TryHitPlayer();
        }

        /// <summary>
        /// Casts a ray in the monitoring direction. If it hits the player, player dies.
        /// </summary>
        private void TryHitPlayer()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, _monitorDirection.ToVector2(), 
                float.MaxValue, (int)CollisionAssistant.VisibleToEnemy);
            if (hit.collider.gameObject == LevelManager.Instance.playerObject) GameManager.PlayerLoss();
        }
        
        /// <summary>
        /// Align visually with the concrete monitor direction
        /// </summary>
        private void AlignWithMonitorDirection()
        {
            Vector2 monitorDirActual = _monitorDirection.ToVector2();
            Vector3 curRot = transform.eulerAngles;
            curRot.z = Mathf.Atan2(monitorDirActual.y, monitorDirActual.x) * Mathf.Rad2Deg + 90f;
            transform.eulerAngles = curRot;
        }
    }
}

// Have some desired direction, and some current direction
// Rotate by minimum of angle to desired direction and turn limit * deltatime