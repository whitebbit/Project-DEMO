using System;
using _Game.Scripts.Controllers.Inputs;
using _Game.Scripts.Controllers.Interfaces;
using UnityEngine;

namespace _Game.Scripts.Player
{
    public class PlayerUnit : MonoBehaviour
    {
        #region FIELDS SERIALIZED

        [SerializeField] private PlayerMovement movement;

        private IInput _input;

        #endregion

        #region FIELDS

        #endregion

        #region UNITY FUNCTIONS

        private void Awake()
        {
            _input = new DesktopInput();
        }

        private void Update()
        {
            movement.Move(_input.GetHorizontalAxis, _input.GetVerticalAxis);
        }

        #endregion

        #region METHODS

        #endregion
    }
}