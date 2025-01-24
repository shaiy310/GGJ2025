using MyFiles.Scripts.Events;
using SuperMaxim.Messaging;
using UnityEngine;

namespace MyFiles.Scripts
{
	public class ObstacleMover : MonoBehaviour
	{
		private static float MinX => LevelManager.Instance.MinX;
		private static float MaxX => LevelManager.Instance.MaxX;
		private const float MaxSpeed = 4f;
		private const float SpeedChangePerSecond = 16f;

		private float _inputDirection;
		private float _speed;

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
			_speed = Mathf.MoveTowards(_speed, _inputDirection * MaxSpeed, SpeedChangePerSecond * Time.deltaTime);
			transform.Translate(_speed * Time.deltaTime, 0, 0, Space.World);
			HandleOutOfBound();
		}

		private void HandleOutOfBound()
		{
			if (transform.position.x < MinX)
			{
				transform.Translate(MaxX - MinX, 0, 0, Space.World);
			}
			else if (transform.position.x > MaxX)
			{
				transform.Translate(MinX - MaxX, 0, 0, Space.World);
			}
		}
	}
}