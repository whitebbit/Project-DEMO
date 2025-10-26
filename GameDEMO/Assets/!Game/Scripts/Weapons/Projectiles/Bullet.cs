using System;
using UnityEngine;

namespace _Game.Scripts.Weapons.Projectiles
{
    public class Bullet : MonoBehaviour
    {
        #region FIELDS SERIALIZED

        [SerializeField] private new Rigidbody rigidbody;

        #endregion

        #region FIELDS

        #endregion

        #region UNITY FUNCTIONS

        #endregion

        #region METHODS

        public void Initialize(Vector3 velocity)
        {
            rigidbody.velocity = velocity;
            Destroy(gameObject, 2.5f);
        }

        private void OnCollisionEnter(Collision other)
        {
            Destroy(gameObject);
        }

        #endregion
    }
}