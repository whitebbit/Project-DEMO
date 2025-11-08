using System.Collections.Generic;
using _Game.Scripts.Multiplayer;
using _Game.Scripts.Units.Interfaces;
using UnityEngine;

namespace _Game.Scripts.Units
{
    public abstract class UnitMovement : MonoBehaviour, IMovement
    {
        #region FIELDS SERIALIZED

        [SerializeField] protected Unit unit;

        #endregion

        #region FIELDS

        public abstract float Speed { get;  }
        public abstract Vector3 Velocity { get; protected set; }

        #endregion

        #region UNITY FUNCTIONS

        #endregion

        #region METHODS

        public abstract void Move(Vector3 position);
        public abstract void GetMoveInfo(out Vector3 position, out Vector3 velocity);

        public virtual void Jump()
        {
        }

        public virtual void SetMovement(in Vector3 position, in Vector3 velocity, in float averageInterval)
        {
        }

        public virtual void Teleport(Vector3 position)
        {
            
        }
        
        #endregion

        #region GETTERS

        #endregion
    }
}