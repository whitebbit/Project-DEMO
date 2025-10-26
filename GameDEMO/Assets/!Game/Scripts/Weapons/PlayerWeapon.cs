using UnityEngine;

namespace _Game.Scripts.Weapons
{
    public class PlayerWeapon : Weapon
    {

        #region FIELDS SERIALIZED

        [SerializeField] private float bulletSpeed;
        [SerializeField] private float shootDelay;
        [SerializeField] private Transform muzzlePoint;
        
        #endregion

        #region FIELDS

        private float _lastShotTime;
        private static readonly int ShootHash = Animator.StringToHash("Shoot");

        #endregion

        #region UNITY FUNCTIONS

        #endregion

        #region METHODS

        public override bool TryShoot(out ShootInfo info)
        {
            info = new ShootInfo();

            if (Time.time - _lastShotTime < shootDelay) return false;

            var position = muzzlePoint.position;
            var velocity = muzzlePoint.forward * bulletSpeed;

            var bullet = Instantiate(bulletPrefab, position, muzzlePoint.rotation);
            bullet.Initialize(velocity);

            animator.SetTrigger(ShootHash);

            _lastShotTime = Time.time;

            info = InitShootInfo(info, velocity, position);

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
}