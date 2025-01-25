using MyFiles.Scripts.Events;
using SuperMaxim.Messaging;
using UnityEngine;
using UnityEngine.Video;

namespace MyFiles.Scripts
{
	public class VideoActivator : MonoBehaviour
	{
		[SerializeField] private VideoPlayer _video1;
		[SerializeField] private VideoPlayer _video2;

		public void PlayVideo()
		{
			_video1.gameObject.SetActive(true);
			_video2.gameObject.SetActive(true);
			_video1.Play();
			_video2.Play();
		}
	}
}