using _Game.Scripts.Controllers;
using _Game.Scripts.Controllers.Inputs;
using _Game.Scripts.Controllers.Interfaces;
using UnityEngine;

namespace _Game.Scripts.Units.Player
{
    public class PlayerController : MonoBehaviour
    {
        #region FIELDS SERIALIZED

        [SerializeField] private UnitMovement movement;
        [SerializeField] private CameraLook cameraLook;

        private IInput _input;

        #endregion

        #region FIELDS

        #endregion

        #region UNITY FUNCTIONS

        private void Awake()
        {
            _input = new DesktopInput();
        }

        private void FixedUpdate()
        {
            movement.Move(new Vector3(_input.GetHorizontalAxis, 0, _input.GetVerticalAxis));
        }

        private void Update()
        {
            cameraLook.RotateX(-_input.GetMouseYAxis);
            cameraLook.RotateY(_input.GetMouseXAxis);

            if (_input.GetJumpKeyDown)
                movement.Jump();
        }

        #endregion

        #region METHODS

        #endregion
    }
}