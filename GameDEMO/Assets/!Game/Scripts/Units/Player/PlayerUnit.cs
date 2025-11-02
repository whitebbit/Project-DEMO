using System.Collections.Generic;
using Colyseus.Schema;
using UnityEngine;

namespace _Game.Scripts.Units.Player
{
    public class PlayerUnit : Unit
    {
        #region FIELDS SERIALIZED

        [SerializeField] private UnitController controller;

        #endregion

        #region FIELDS

        #endregion

        #region UNITY FUNCTIONS

        #endregion

        #region METHODS

        public override void Initialize(string id, global::Player player)
        {
            player.OnChange += controller.OnChange;
        }

        #endregion
    }
}