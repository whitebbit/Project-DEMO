using System.Collections.Generic;
using _Game.Scripts.Multiplayer;
using _Game.Scripts.Units.Interfaces;
using UnityEngine;

namespace _Game.Scripts.Units
{
    public abstract class UnitMovement : MonoBehaviour, IMovement
    {
        #region FIELDS SERIALIZED

        [SerializeField] private float speed = 5;

        #endregion

        #region FIELDS
        
        public float Speed => speed;

        #endregion

        #region UNITY FUNCTIONS
        
        #endregion

        #region METHODS

        public abstract void Move(Vector3 position);

        #endregion

        #region GETTERS

        #endregion
    }
}