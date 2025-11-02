using System;
using System.Collections.Generic;
using _Game.Scripts.Weapons;
using Colyseus.Schema;
using UnityEngine;

namespace _Game.Scripts.Units
{
    public abstract class UnitController : MonoBehaviour
    {
        #region FIELDS SERIALIZED

        [SerializeField] protected Unit unit;
        
        #endregion

        #region FIELDS

        #endregion

        #region UNITY FUNCTIONS

        protected virtual void Update()
        {
            HandleMovement();
        }

        #endregion

        #region METHODS

        public virtual void Shoot(in ShootInfo info)
        {
        }
        
        protected virtual void HandleMovement()
        {
            
        }
        
        public virtual void OnChange(List<DataChange> changes)
        {
            
        }
        
        #endregion
        
    }
}