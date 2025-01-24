using UnityEngine;

namespace MyFiles.Scripts
{
	public class LevelManager : MonoBehaviour
	{
		private Bounds _bounds;

		public float MinX => _bounds.min.x - _bounds.center.x;
		public float MaxX => _bounds.max.x - _bounds.center.x;

		public static LevelManager Instance;

		private void Awake()
		{
			Instance = this;
		}

		private void Start()
		{
			SetBounds();
		}

		private void SetBounds()
		{
			_bounds = new Bounds();
			var obstacleMovers = FindObjectsOfType<ObstacleMover>();
			foreach (var obstacleMover in obstacleMovers)
			{
				var spriteRenderer = obstacleMover.GetComponent<SpriteRenderer>();
				_bounds.Encapsulate(spriteRenderer.bounds);
			}
		}
	}
}