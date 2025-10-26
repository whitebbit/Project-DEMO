using System;
using _Game.Scripts.Weapons.Projectiles;
using UnityEngine;

namespace _Game.Scripts.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        #region FIELDS SERIALIZED

        [SerializeField] protected Animator animator;
        [SerializeField] protected Bullet bulletPrefab;

        #endregion

        #region FIELDS

        #endregion

        #region UNITY FUNCTIONS

        #endregion

        #region METHODS

        public virtual bool TryShoot(out ShootInfo info)
        {
            info = new ShootInfo();
            return false;
        }

        public virtual void ShootByInfo(ShootInfo info){}

        #endregion
    }

    [Serializable]
    public struct ShootInfo
    {
        public string key;

        public float dX;
        public float dY;
        public float dZ;

        public float pX;
        public float pY;
        public float pZ;

        public Vector3 GetDirection()
        {
            return new Vector3(dX, dY, dZ);
        }

        public Vector3 GetPosition()
        {
            return new Vector3(pX, pY, pZ);
        }
    }
}