using System;
using _Game.Scripts.Units.Interfaces;
using UnityEngine;

namespace _Game.Scripts.Weapons.Projectiles
{
    public class Bullet : MonoBehaviour
    {
        #region FIELDS SERIALIZED

        [SerializeField] private new Rigidbody rigidbody;

        #endregion

        #region FIELDS

        private int _damage;

        #endregion

        #region UNITY FUNCTIONS

        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.TryGetComponent(out IDamageable damageable))
            {
                damageable.ApplyDamage(_damage);
            }

            Destroy(gameObject);
        }

        #endregion

        #region METHODS

        public void Initialize(Vector3 velocity, int damage = 0)
        {
            _damage = damage;
            rigidbody.velocity = velocity;
            Destroy(gameObject, 2.5f);
        }

        #endregion
    }
}