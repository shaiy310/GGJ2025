using System;
using UnityEngine;

namespace MyFiles.Scripts
{
	public class LevelManager : MonoBehaviour
	{
		public static LevelManager Instance { get; private set; }

		[SerializeField] GameObject[] levels;
		
		public int Level { get; private set; }
		public float MinX => _bounds.min.x - _bounds.center.x;
		public float MaxX => _bounds.max.x - _bounds.center.x;

		private Bounds _bounds;

		private void Awake()
		{
			Instance = this;
		}

		private void Start()
		{
			LoadLevel(0);
		}

        private void LoadLevel(int level)
        {
            Level = level;
			for (int i = 0; i < levels.Length; i++) {
				levels[i].SetActive(i == level);
			}
			SetBounds();
        }

        private void SetBounds()
		{
			_bounds = new Bounds();
			var obstacleMovers = levels[Level].GetComponentsInChildren<ObstacleMover>();
			foreach (var obstacleMover in obstacleMovers)
			{
				var spriteRenderer = obstacleMover.GetComponent<SpriteRenderer>();
				_bounds.Encapsulate(spriteRenderer.bounds);
			}
		}
	}
}