using System;
using _Game.Scripts.Weapons.Projectiles;
using UnityEngine;

namespace _Game.Scripts.Weapons
{
    public class Weapon : MonoBehaviour
    {
        private static readonly int ShootHash = Animator.StringToHash("Shoot");

        #region FIELDS SERIALIZED

        [SerializeField] private float bulletSpeed;
        [SerializeField] private float shootDelay;

        [SerializeField] private Animator animator;
        [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private Transform muzzlePoint;

        #endregion

        #region FIELDS

        private float _lastShotTime;

        #endregion

        #region UNITY FUNCTIONS

        #endregion

        #region METHODS

        public bool TryShoot(out ShootInfo info)
        {
            info = new ShootInfo(); 

            if (Time.time - _lastShotTime < shootDelay) return false;

            var position = muzzlePoint.position;
            var direction = muzzlePoint.forward;
            
            var bullet = Instantiate(bulletPrefab, position, muzzlePoint.rotation);
            bullet.Initialize(direction, bulletSpeed);

            animator.SetTrigger(ShootHash);

            _lastShotTime = Time.time;

            info = InitShootInfo(info, direction, position);

            return true;
        }

        private ShootInfo InitShootInfo(ShootInfo info, Vector3 direction, Vector3 position)
        {
            direction *= bulletSpeed;

            info.pX = position.x;
            info.pY = position.y;
            info.pZ = position.z;
            
            info.dX = direction.x;
            info.dY = direction.y;
            info.dZ = direction.z;
            
            return info;
        }

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
    }
}