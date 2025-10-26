using System;
using UnityEngine;

namespace _Game.Scripts.Controllers
{
    public class GroundChecker : MonoBehaviour
    {
        #region FIELDS SERIALIZED

        [SerializeField] private LayerMask layerMask;
        [SerializeField] private float radius;
        [SerializeField] private float coyoteTime = 0.15f;

        #endregion

        #region FIELDS

        public bool IsGrounded { get; private set; }

        private float _flyTimer;

        #endregion

        #region UNITY FUNCTIONS

        private void Update()
        {
            if (Physics.CheckSphere(transform.position, radius, layerMask))
            {
                IsGrounded = true;
                _flyTimer = 0;
            }
            else
            {
                _flyTimer += Time.deltaTime;
                if (_flyTimer >= coyoteTime)
                    IsGrounded = false;
            }

        }

        #endregion

        #region METHODS

        #endregion

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, radius);
        }
#endif
    }
}