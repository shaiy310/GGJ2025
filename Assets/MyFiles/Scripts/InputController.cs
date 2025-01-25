using MyFiles.Scripts.Events;
using SuperMaxim.Messaging;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MyFiles.Scripts
{
	public class InputController : MonoBehaviour
	{
		public void OnHorizontalMovement(InputAction.CallbackContext context)
		{
			Messenger.Default.Publish(new InputEvent(context.ReadValue<float>()));
		}

		public void OnPushDown(InputAction.CallbackContext context)
		{
            Messenger.Default.Publish(new PushDownEvent(context.ReadValueAsButton()));
        }

        public void OnEscape(InputAction.CallbackContext context)
        {
			Messenger.Default.Publish(new EscapeEvent());
        }
    }
}
