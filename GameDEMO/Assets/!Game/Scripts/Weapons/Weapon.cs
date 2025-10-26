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

        public void Shoot()
        {
            if(Time.time - _lastShotTime < shootDelay) return;
            
            var bullet = Instantiate(bulletPrefab, muzzlePoint.position, muzzlePoint.rotation);
            bullet.Initialize(muzzlePoint.forward, bulletSpeed);
            
            animator.SetTrigger(ShootHash);
            
            _lastShotTime = Time.time;
        }
        
        #endregion

        
    }
}