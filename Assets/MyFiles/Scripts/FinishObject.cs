using MyFiles.Scripts.Events;
using SuperMaxim.Messaging;
using UnityEngine;

namespace MyFiles.Scripts
{
	public class FinishObject : MonoBehaviour
	{
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.gameObject.tag == "Bubble")
			{
				Destroy(other.gameObject);
				// todo - add score.
				Messenger.Default.Publish(new BubbleFinishedEvent());
			}
		}
	}
}
