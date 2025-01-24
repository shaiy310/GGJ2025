using UnityEngine;

namespace MyFiles.Scripts
{
	public class DontDestroy : MonoBehaviour
	{
		private void Start()
		{
			DontDestroyOnLoad(gameObject);
		}
	}
}