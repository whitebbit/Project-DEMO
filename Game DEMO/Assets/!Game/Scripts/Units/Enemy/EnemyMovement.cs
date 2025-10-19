using UnityEngine;

namespace _Game.Scripts.Units.Enemy
{
    public class EnemyMovement : UnitMovement
    {
        #region FIELDS SERIALIZED

        #endregion

        #region FIELDS

        #endregion

        #region UNITY FUNCTIONS

        #endregion

        #region METHODS

        public override void Move(Vector3 position)
        {
            transform.position = position;
        }
        
        #endregion
    }
}