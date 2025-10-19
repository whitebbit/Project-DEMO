using System.Collections.Generic;
using Colyseus.Schema;
using UnityEngine;

namespace _Game.Scripts.Units.Enemy
{
    public class EnemyUnit : MonoBehaviour
    {
        #region FIELDS SERIALIZED

        [SerializeField] private UnitMovement movement;

        #endregion

        #region FIELDS

        #endregion

        #region UNITY FUNCTIONS

        #endregion

        #region METHODS

        public void OnChange(List<DataChange> changes)
        {
            var position = transform.position;
            foreach (var change in changes)
            {
                switch (change.Field)
                {
                    case "x":
                        position.x = (float)change.Value;
                        break;
                    case "y":
                        position.z = (float)change.Value;
                        break;
                }
            }

            movement.Move(position);
        }

        #endregion
    }
}