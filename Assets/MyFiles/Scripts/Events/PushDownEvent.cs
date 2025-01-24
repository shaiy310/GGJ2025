namespace MyFiles.Scripts.Events
{
    public class PushDownEvent
    {
        public bool IsPressed { get; private set; }

        public PushDownEvent(bool isPressed)
        {
            IsPressed = isPressed;
        }
    }
}
