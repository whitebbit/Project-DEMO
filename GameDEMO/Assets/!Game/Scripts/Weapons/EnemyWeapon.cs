using UnityEngine;

namespace _Game.Scripts.Weapons
{
    public class EnemyWeapon : Weapon
    {
        #region FIELDS SERIALIZED

        #endregion

        #region FIELDS

        #endregion

        #region UNITY FUNCTIONS

        #endregion

        #region METHODS

        public override void ShootByInfo(ShootInfo info)
        {
            var position = info.GetPosition();
            var velocity = info.GetDirection();
            
            var bullet = Instantiate(bulletPrefab, position, Quaternion.identity);

            bullet.Initialize(velocity);
        }

        #endregion
    }
}