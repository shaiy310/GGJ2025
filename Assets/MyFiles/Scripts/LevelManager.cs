using System;
using UnityEngine;

namespace MyFiles.Scripts
{
	[Serializable]
	struct Level
	{
		public string Name;
		public float time;
		public int scoreThreshold;
		public GameObject map;
	}

	public class LevelManager : MonoBehaviour
	{
		public static LevelManager Instance { get; private set; }

		[SerializeField] Timer timer;
		[SerializeField] Level[] levels;
		int score = 0; // TBD
		
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
            timer.OnTimerEnd += OnEndLevel;
			LoadLevel(0);
		}

        private void LoadLevel(int level)
        {
            Level = level;
			for (int i = 0; i < levels.Length; i++) {
				levels[i].map.SetActive(i == level);
			}
			SetBounds();
			timer.StartTimer(levels[level].time);
        }

        private void OnEndLevel()
        {
            // check score
			if (score >= levels[Level].scoreThreshold) {
				if (Level + 1 < levels.Length) {
					LoadLevel(Level + 1);
				} else {
					// end all levels
				}
            } else {
				LoadLevel(Level);
			}
        }

        private void SetBounds()
		{
			_bounds = new Bounds();
			var obstacleMovers = levels[Level].map.GetComponentsInChildren<ObstacleMover>();
			foreach (var obstacleMover in obstacleMovers)
			{
				var spriteRenderer = obstacleMover.GetComponent<SpriteRenderer>();
				_bounds.Encapsulate(spriteRenderer.bounds);
			}
		}
	}
}