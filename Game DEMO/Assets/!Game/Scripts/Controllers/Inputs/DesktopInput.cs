using _Game.Scripts.Controllers.Interfaces;
using UnityEngine;

namespace _Game.Scripts.Controllers.Inputs
{
    public class DesktopInput : IInput
    {
        public float GetHorizontalAxis => Input.GetAxis("Horizontal");
        public float GetVerticalAxis => Input.GetAxis("Vertical");

        public float GetMouseXAxis => Input.GetAxis("Mouse X");
        public float GetMouseYAxis => Input.GetAxis("Mouse Y");

        public bool GetJumpKeyDown => Input.GetKeyDown(KeyCode.Space);
    }
}