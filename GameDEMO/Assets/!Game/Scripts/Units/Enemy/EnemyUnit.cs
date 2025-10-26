using System;
using System.Collections.Generic;
using System.Linq;
using Colyseus.Schema;
using UnityEngine;

namespace _Game.Scripts.Units.Enemy
{
    public class EnemyUnit : Unit
    {
        #region FIELDS SERIALIZED

        [SerializeField] private EnemyController controller;
        [SerializeField] private EnemyMovement movement;

        #endregion

        #region FIELDS

        private global::Player _player;
        
        #endregion

        #region UNITY FUNCTIONS

        #endregion

        #region METHODS
        
        public override void Initialize(global::Player player)
        {
            _player = player;
            
            movement.SetSpeed(_player.speed);
            _player.OnChange += controller.OnChange;
        }

        public override void Destroy()
        {
            _player.OnChange -= controller.OnChange;
            base.Destroy();
        }

        #endregion
        
    }
}