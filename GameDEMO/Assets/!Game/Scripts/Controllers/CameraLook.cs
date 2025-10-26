using System;
using UnityEngine;

namespace _Game.Scripts.Controllers
{
    public class CameraLook : MonoBehaviour
    {
        #region FIELDS SERIALIZED

        [SerializeField] private float sensitivity = 2f;
        [SerializeField] private Vector2 clampAngles = new(-90, 90);
        [SerializeField] private Transform rootX;
        [SerializeField] private Transform cameraPoint;

        #endregion

        #region FIELDS

        private Rigidbody _rigidbody;
        private float _rotateY;
        private float _currentRotateX;

        #endregion

        #region UNITY FUNCTIONS

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            if (!Camera.main) return;
            
            var cam = Camera.main.transform;

            if (!cam) return;
            
            cam.SetParent(cameraPoint);
            cam.localPosition = Vector3.zero;
            cam.localRotation = Quaternion.identity;
        }

        private void FixedUpdate()
        {
            ApplyRotation();
        }

        #endregion

        #region METHODS

        public void RotateX(float value)
        {
            _currentRotateX = Mathf.Clamp(_currentRotateX + value * sensitivity, clampAngles.x, clampAngles.y);
            rootX.localEulerAngles = new Vector3(_currentRotateX, 0f, 0f);
        }

        public void RotateY(float value)
        {
            _rotateY += value * sensitivity;
        }

        private void ApplyRotation()
        {
            _rigidbody.angularVelocity = new Vector3(0, _rotateY, 0);
            _rotateY = 0;
        }

        public void GetRotateInfo(out Vector2 rotation)
        {
            rotation = new Vector2(rootX.localEulerAngles.x, transform.eulerAngles.y);
        }

        #endregion
    }
}