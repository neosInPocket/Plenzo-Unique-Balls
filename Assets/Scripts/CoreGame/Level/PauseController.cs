using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
	[SerializeField] private RailController railController;
	[SerializeField] private PlayerMovement playerMovement;
	[SerializeField] private CountingScreen countingScreen;
	public bool Enabled { get; set; }


	public void Pause()
	{
		if (!Enabled) return;
		gameObject.SetActive(true);
		railController.Enabled = false;
		playerMovement.Enabled = false;
	}

	public void UnPause()
	{
		countingScreen.EndAction += OnUnPauseEnd;
		gameObject.SetActive(false);
		countingScreen.Play();
	}

	private void OnUnPauseEnd()
	{
		countingScreen.EndAction -= OnUnPauseEnd;
		railController.Enabled = true;
		playerMovement.Enabled = true;

	}

	public void LoadMenu()
	{
		SceneManager.LoadScene("Menu");
	}
}
