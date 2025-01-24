using System;
using MyFiles.Scripts.Events;
using SuperMaxim.Messaging;
using UnityEngine;

namespace MyFiles.Scripts
{
	public class ScoreSystem : MonoBehaviour
	{
		private int _score;
		private void OnEnable()
		{
			Messenger.Default.Subscribe<BubbleFinishedEvent>(OnBubbleFinished);
		}

		private void OnDisable()
		{
			Messenger.Default.Unsubscribe<BubbleFinishedEvent>(OnBubbleFinished);
		}

		private void OnStartNewLevel()
		{
			_score = 0;
		}

		private void OnBubbleFinished(BubbleFinishedEvent bubbleFinishedEvent)
		{
			_score++;
			Messenger.Default.Publish(new ScoreChangedEvent(_score));
		}
	}
}