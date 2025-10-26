using System;
using UnityEngine;

namespace _Game.Scripts.Controllers
{
    public class GroundChecker : MonoBehaviour
    {
        #region FIELDS SERIALIZED

        #endregion

        #region FIELDS

        public bool IsGrounded { get; private set; }

        #endregion

        #region UNITY FUNCTIONS

        private void OnCollisionStay(Collision other)
        {
            var contactPoints = other.contacts;
            foreach (var contact in contactPoints)
            {
                if (contact.normal.y > 0.45f)
                    IsGrounded = true;
            }
        }

        private void OnCollisionExit(Collision other)
        {
            IsGrounded = false;
        }

        #endregion

        #region METHODS

        #endregion
    }
}