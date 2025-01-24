using System;
using MyFiles.Scripts.Events;
using SuperMaxim.Messaging;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MyFiles.Scripts.UI
{
	public class UIScore : MonoBehaviour
	{
		[SerializeField] private TMP_Text _text;
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
			_text.text = scoreChangedEvent.Score.ToString();
		}
	}
}