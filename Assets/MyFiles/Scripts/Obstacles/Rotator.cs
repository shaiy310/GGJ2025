using UnityEngine;

namespace MyFiles.Scripts.Obstacles
{
	public class Rotator : MonoBehaviour
	{
		[Tooltip("the rotation speed in angles per second")]
		[SerializeField] private float _speed = 1f;
		private void Update()
		{
			transform.Rotate(0,0,_speed * Time.deltaTime, Space.Self);
		}
	}
}
