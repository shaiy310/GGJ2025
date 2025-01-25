using MyFiles.Scripts.Events;
using SuperMaxim.Messaging;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
		[SerializeField] TextMeshProUGUI levelNameTxt;
		[SerializeField] TextMeshProUGUI thresholdTxt;
		int score = 0; // TBD
		
		public int Level { get; private set; }
		public float MinX => _bounds.min.x - _bounds.center.x;
		public float MaxX => _bounds.max.x - _bounds.center.x;

		private Bounds _bounds;

		[SerializeField] private VideoActivator _videoActivator;
		[SerializeField] private Image _fadeImage;
		[SerializeField] private float _fadeInTime = 0.2f; 
		[SerializeField] private float _fadeOutTime = 0.3f; 

		private void Awake()
		{
			Instance = this;
		}

        private void OnEnable()
        {
            Messenger.Default.Subscribe<ScoreChangedEvent>(OnScoreChanged);
        }

        private void OnDisable()
        {
            Messenger.Default.Unsubscribe<ScoreChangedEvent>(OnScoreChanged);
        }

        private void OnScoreChanged(ScoreChangedEvent scoreChangedEvent)
        {
            score = scoreChangedEvent.Score;
			if (score == levels[Level].scoreThreshold) {
				OnEndLevel();

            }
        }

        private void Start()
		{
            timer.OnTimerEnd += OnEndLevel;
			LoadLevel(0);
		}

        private void LoadLevel(int level)
        {
	        StartCoroutine(LoadLevelEnumerator(level));
        }

        private IEnumerator LoadLevelEnumerator(int level)
        {
	        //fade in
	        _videoActivator.PlayVideo();
	        yield return StartCoroutine(FadeEnumerator(0,1,_fadeInTime));
	        
	        //load the actual level
	        Level = level;
	        for (int i = 0; i < levels.Length; i++) {
		        levels[i].map.SetActive(i == level);
	        }
	        SetBounds();
	        levelNameTxt.text = levels[Level].Name;
			thresholdTxt.text = $"/{levels[Level].scoreThreshold}";

            timer.StartTimer(levels[Level].time);
	        Messenger.Default.Publish(new NewLevelEvent());
	        
	        //fade out
	        yield return StartCoroutine(FadeEnumerator(1,0,_fadeOutTime));
        }

        private IEnumerator FadeEnumerator(float from, float to, float time)
        {
	        var startTime = Time.time;
	        var endTime = startTime + time;
	        while (Time.time < endTime)
	        {
		        var t = 1 - (endTime - Time.time) / time;
		        var color = _fadeImage.color;
		        color.a = Mathf.Lerp(from, to, t);
		        _fadeImage.color = color;
		        yield return null;
	        }
	        var color2 = _fadeImage.color;
	        color2.a = to;
	        _fadeImage.color = color2;
        }

        private void OnEndLevel()
        {
            // check score
			if (score >= levels[Level].scoreThreshold) {
				if (Level + 1 < levels.Length) {
					LoadLevel(Level + 1);
				} else {
					// end all levels
					Debug.Log("finished all levels");
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
				var spriteRenderers = obstacleMover.GetComponentsInChildren<SpriteRenderer>();
				foreach (var spriteRenderer in spriteRenderers)
				{
					_bounds.Encapsulate(spriteRenderer.bounds);
				}
			}
		}
	}
}