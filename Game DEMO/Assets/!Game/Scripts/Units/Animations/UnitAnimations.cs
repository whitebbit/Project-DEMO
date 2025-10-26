using System;
using _Game.Scripts.Controllers;
using UnityEngine;

namespace _Game.Scripts.Units.Animations
{
    public class UnitAnimations : MonoBehaviour
    {
        #region FIELDS SERIALIZED

        [SerializeField] private Animator animator;
        [SerializeField] private UnitMovement movement;
        [SerializeField] private GroundChecker groundChecker;

        #endregion

        #region FIELDS

        private static readonly int Grounded = Animator.StringToHash("Grounded");
        private static readonly int Speed = Animator.StringToHash("Speed");

        #endregion

        #region UNITY FUNCTIONS

        private void Update()
        {
            var localVelocity = movement.transform.InverseTransformVector(movement.Velocity);
            var speed = localVelocity.magnitude / movement.Speed;
            var sign = Mathf.Sign(localVelocity.z);

            animator.SetBool(Grounded, groundChecker.IsGrounded);
            animator.SetFloat(Speed, groundChecker.IsGrounded ? speed * sign : 0);
        }

        #endregion

        #region METHODS

        #endregion
    }
}