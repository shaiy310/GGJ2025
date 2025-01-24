namespace MyFiles.Scripts.Events
{
	public class InputEvent
	{
		public float HorizontalMovement;

		public InputEvent(float horizontalMovement)
		{
			HorizontalMovement = horizontalMovement;
		}
	}

    public class PushDownEvent
    {
        public bool IsPressed { get; private set; }

        public PushDownEvent(bool isPressed)
        {
            IsPressed = isPressed;
        }
    }
}
