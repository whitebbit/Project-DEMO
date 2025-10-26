using UnityEngine;

namespace _Game.Scripts.Units.Enemy
{
    public class EnemyLook : MonoBehaviour
    {

        #region FIELDS SERIALIZED

        [SerializeField] private Transform rootX;
        
        #endregion

        #region FIELDS

        private float _targetX;
        private float _targetY;
        private float _currentVelX;
        private float _currentVelY;
        
        #endregion

        #region UNITY FUNCTIONS
        private void Update()
        {
            var newX = Mathf.SmoothDampAngle(rootX.localEulerAngles.x, _targetX, ref _currentVelX, 0.1f);
            var newY = Mathf.SmoothDampAngle(transform.localEulerAngles.y, _targetY, ref _currentVelY, 0.1f);

            rootX.localEulerAngles = new Vector3(newX, 0, 0);
            transform.localEulerAngles = new Vector3(0, newY, 0);
        }
        #endregion

        #region METHODS

        public void SetRotateX(float value) => _targetX = value;
        public void SetRotateY(float value) => _targetY = value;
        
        #endregion

    }
}