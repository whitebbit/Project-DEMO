using System;
using UnityEngine;

namespace _Game.Scripts.Weapons.Attachments
{
    public class WeaponLaser : MonoBehaviour
    {
        #region FIELDS SERIALIZED

        [SerializeField] private LayerMask layerMask;
        [SerializeField] private Transform root;
        [SerializeField] private Transform point;
        [SerializeField] private float pointSize;

        #endregion

        #region FIELDS

        private Transform _camera;

        #endregion

        #region UNITY FUNCTIONS

        private void Awake()
        {
            if (Camera.main) _camera = Camera.main.transform;
        }

        private void Update()
        {
            var ray = new Ray(root.position, root.forward);
            Debug.DrawRay(ray.origin, ray.direction, Color.red);
            if (!Physics.Raycast(ray, out var hit, 100, layerMask, QueryTriggerInteraction.Ignore)) return;

            var distance = Vector3.Distance(_camera.position, hit.point);

            root.localScale = new Vector3(1, 1, hit.distance);
            point.position = hit.point;
            point.localScale = Vector3.one * (distance * pointSize);
        }

        #endregion

        #region METHODS

        #endregion
    }
}