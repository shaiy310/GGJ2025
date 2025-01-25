using MyFiles.Scripts.Events;
using SuperMaxim.Messaging;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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

        [SerializeField] Level[] levels;
        [SerializeField] Timer timer;
        [SerializeField] TextMeshProUGUI levelNameTxt;
        [SerializeField] TextMeshProUGUI thresholdTxt;


        public static int CurrentLevel { get; private set; } = 0;
        public float MinX => _bounds.min.x - _bounds.center.x;
        public float MaxX => _bounds.max.x - _bounds.center.x;

        private Bounds _bounds;
        int score = 0; // TBD
        bool firstLoad;

        [SerializeField] private VideoActivator _videoActivator;
        [SerializeField] private Image _fadeImage;
        [SerializeField] private float _fadeInTime = 0.2f; 
        [SerializeField] private float _fadeOutTime = 0.3f;
        [SerializeField] private AudioEvent _levelTransitionAudioEvent;
        [SerializeField] private AudioEvent _levelWinAudioEvent;

        private void Awake()
        {
            Instance = this;
            firstLoad = true;
        }

        private void OnEnable()
        {
            Messenger.Default.Subscribe<ScoreChangedEvent>(OnScoreChanged);
            Messenger.Default.Subscribe<EscapeEvent>(OnEscape);
        }

        private void OnDisable()
        {
            Messenger.Default.Unsubscribe<ScoreChangedEvent>(OnScoreChanged);
            Messenger.Default.Unsubscribe<EscapeEvent>(OnEscape);
        }

        void OnEscape(EscapeEvent escapeEvent)
        {
            SceneManager.LoadScene(0);
        }

        private void OnScoreChanged(ScoreChangedEvent scoreChangedEvent)
        {
            score = scoreChangedEvent.Score;
            if (score == levels[CurrentLevel].scoreThreshold) {
                OnEndLevel();
            }
        }

        private void Start()
        {
            timer.OnTimerEnd += OnEndLevel;
            LoadLevel(CurrentLevel);
        }

        private void LoadLevel(int level)
        {
            StartCoroutine(LoadLevelEnumerator(level));
        }

        private IEnumerator LoadLevelEnumerator(int level)
        {
            //fade in
#if !UNITY_WEBGL
            _videoActivator.PlayVideo();
#endif
            _levelTransitionAudioEvent.Play();
            if (firstLoad) {
                yield return StartCoroutine(FadeEnumerator(1, 1, _fadeInTime));
                firstLoad = false;
            } else {
                yield return StartCoroutine(FadeEnumerator(0, 1, _fadeInTime));
            }

            //load the actual level
            CurrentLevel = level;
            for (int i = 0; i < levels.Length; i++) {
                levels[i].map.SetActive(i == level);
            }
            SetBounds();
            levelNameTxt.text = levels[CurrentLevel].Name;
            thresholdTxt.text = $"/{levels[CurrentLevel].scoreThreshold}";

            timer.StartTimer(levels[CurrentLevel].time);
            Messenger.Default.Publish(new NewLevelEvent());
            
            //fade out
            yield return StartCoroutine(FadeEnumerator(1, 0, _fadeOutTime));
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
            if (score >= levels[CurrentLevel].scoreThreshold)
            {
                OnLevelWin();
            } else {
                LoadLevel(CurrentLevel);
            }
        }

        private void OnLevelWin()
        {
            StartCoroutine(OnLevelWinEnumerator());
        }

        private IEnumerator OnLevelWinEnumerator()
        {
            _levelWinAudioEvent.Play();
            MusicScript.Instance.Pause();

            yield return new WaitForSeconds(2);

            MusicScript.Instance.UnPause();

            if (CurrentLevel + 1 < levels.Length) {
                LoadLevel(CurrentLevel + 1);
            } else {
                // end all levels
                Debug.Log("finished all levels");
                SceneManager.LoadScene(0);
            }
        }

        private void SetBounds()
        {
            _bounds = new Bounds();
            var obstacleMovers = levels[CurrentLevel].map.GetComponentsInChildren<ObstacleMover>();
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