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
			Messenger.Default.Subscribe<NewLevelEvent>(OnStartNewLevel);
		}

		private void OnDisable()
		{
			Messenger.Default.Unsubscribe<BubbleFinishedEvent>(OnBubbleFinished);
            Messenger.Default.Unsubscribe<NewLevelEvent>(OnStartNewLevel);
        }

		private void OnStartNewLevel(NewLevelEvent newLevelEvent)
		{
			_score = 0;
            Messenger.Default.Publish(new ScoreChangedEvent(_score));
        }

		private void OnBubbleFinished(BubbleFinishedEvent bubbleFinishedEvent)
		{
			_score++;
			Messenger.Default.Publish(new ScoreChangedEvent(_score));
		}
	}
}