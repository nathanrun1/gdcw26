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
        
        [Header("References")]
        [SerializeField] LineRenderer _lineRenderer;
        [SerializeField] Transform _shotOrigin;

        protected override void OnValidate()
        {
            base.OnValidate();
            AlignWithMonitorDirection();
            _lineRenderer.startColor = _maskColor.GetColor();
            _lineRenderer.endColor = _maskColor.GetColor();
        }

        private void Awake()
        {
            AlignWithMonitorDirection();
            _lineRenderer.startColor = _maskColor.GetColor();
            _lineRenderer.endColor = _maskColor.GetColor();
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
            if (!_isEngaged) return;
            Vector2 shotOrigin = _shotOrigin.position;
            Vector2 shotDir = _monitorDirection.ToVector2();
            
            RaycastHit2D hit = Physics2D.Raycast(shotOrigin, shotDir, 
                float.MaxValue, (int)CollisionAssistant.VisibleToEnemy);
            if (hit && hit.collider.gameObject == LevelManager.Instance.playerObject) GameManager.PlayerLoss();

            DisplayShotLine(shotOrigin, shotDir, hit);
        }

        /// <summary>
        /// Displays a visible "shot line" along the shooter's line of sight
        /// </summary>
        private void DisplayShotLine(Vector3 shotOrigin, Vector2 shotDir, RaycastHit2D hit)
        {
            _lineRenderer.SetPosition(0, shotOrigin);
            if (hit)
                _lineRenderer.SetPosition(1, hit.point);
            else
                _lineRenderer.SetPosition(1, shotOrigin + (Vector3)shotDir * 100f);
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

        protected override void SetEngaged(bool engaged)
        {
            base.SetEngaged(engaged);
            _lineRenderer.enabled = engaged;
        }
    }
}

// Have some desired direction, and some current direction
// Rotate by minimum of angle to desired direction and turn limit * deltatime