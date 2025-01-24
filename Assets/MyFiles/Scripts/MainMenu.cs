using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MyFiles.Scripts
{
	public class MainMenu : MonoBehaviour
	{
		[SerializeField] private Button _startButton;
		[SerializeField] private Button _controlsButton;
		[SerializeField] private Button _gotItButton;
		[SerializeField] private Button _exitButton;
		[SerializeField] private GameObject _controlsScreen;

		private void OnEnable()
		{
			_startButton.onClick.AddListener(StartGame);
			_controlsButton.onClick.AddListener(ShowControls);
			_gotItButton.onClick.AddListener(HideControls);
			_exitButton.onClick.AddListener(ExitGame);
			_controlsScreen.SetActive(false);
		}

		private void OnDisable()
		{
			_startButton.onClick.RemoveListener(StartGame);
			_controlsButton.onClick.RemoveListener(ShowControls);
			_gotItButton.onClick.RemoveListener(HideControls);
			_exitButton.onClick.RemoveListener(ExitGame);
		}

		private static void StartGame()
		{
			SceneManager.LoadScene(1);
		}

		private void ShowControls()
		{
			_controlsScreen.SetActive(true);
		}

		private void HideControls()
		{
			_controlsScreen.SetActive(false);
		}
		
		private void ExitGame()
		{
			Application.Quit();
		}
	}
}
