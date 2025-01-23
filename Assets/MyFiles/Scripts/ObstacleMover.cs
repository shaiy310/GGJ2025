using MyFiles.Scripts.Events;
using SuperMaxim.Messaging;
using UnityEngine;

namespace MyFiles.Scripts
{
	public class ObstacleMover : MonoBehaviour
	{
		private float _inputDirection;
		private const float MinX = -5f;
		private const float MaxX = 5f;
		private const float Speed = 2f;

		private void OnEnable()
		{
			Messenger.Default.Subscribe<InputEvent>(OnInput);
		}

		private void OnDisable()
		{
			Messenger.Default.Unsubscribe<InputEvent>(OnInput);
		}

		private void OnInput(InputEvent inputEvent)
		{
			_inputDirection = inputEvent.HorizontalMovement;
		}

		private void Update()
		{
			Move();
		}

		private void Move()
		{
			if (_inputDirection == 0)
			{
				return;
			}

			transform.Translate(_inputDirection * Speed * Time.deltaTime, 0, 0);
			HandleOutOfBound();
		}

		private void HandleOutOfBound()
		{
			switch (transform.position.x)
			{
				case < MinX:
					transform.Translate(MaxX - MinX, 0, 0);
					break;
				case > MaxX:
					transform.Translate(MinX - MaxX, 0, 0);
					break;
			}
		}
	}
}