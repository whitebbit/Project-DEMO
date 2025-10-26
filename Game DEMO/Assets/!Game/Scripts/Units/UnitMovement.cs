using System.Collections.Generic;
using _Game.Scripts.Multiplayer;
using _Game.Scripts.Units.Interfaces;
using UnityEngine;

namespace _Game.Scripts.Units
{
    public abstract class UnitMovement : MonoBehaviour, IMovement
    {
        #region FIELDS SERIALIZED
        
        #endregion

        #region FIELDS
        
        #endregion

        #region UNITY FUNCTIONS
        
        #endregion

        #region METHODS

        public abstract void Move(Vector3 position);
        public abstract void GetMoveInfo(out Vector3 position, out Vector3 velocity);
        
        public virtual void SetMovement(in Vector3 position, in Vector3 velocity, in float averageInterval)
        {
        }

        #endregion

        #region GETTERS

        #endregion
    }
}