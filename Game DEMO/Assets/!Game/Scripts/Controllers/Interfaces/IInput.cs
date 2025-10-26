namespace _Game.Scripts.Controllers.Interfaces
{
    public interface IInput
    {
        public float GetHorizontalAxis { get; }
        public float GetVerticalAxis { get; }

        public float GetMouseXAxis { get; }
        public float GetMouseYAxis { get; }

        public bool GetJumpKeyDown { get; }
    }
}