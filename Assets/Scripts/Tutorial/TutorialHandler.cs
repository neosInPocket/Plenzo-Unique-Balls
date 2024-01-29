using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;
using UnityEngine.UI;

public class TutorialHandler : MonoBehaviour
{
	[SerializeField] private PlayerMovement player;
	[SerializeField] private RailController rail;
	[SerializeField] private CountingScreen countingScreen;
	[SerializeField] private TMP_Text characterText;
	[SerializeField] private GameObject tutorialWindow;
	[SerializeField] private List<string> quotes;
	[SerializeField] private int goal;
	[SerializeField] private Image fill;
	[SerializeField] private Spawner coinPool;
	[SerializeField] private Spawner spikesPool;
	[SerializeField] private GameObject hand;
	private int currentScore;

	private void Start()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
		Touch.onFingerDown += Quote1;
	}

	private void Quote1(Finger finger)
	{
		Touch.onFingerDown -= Quote1;
		Touch.onFingerDown += Quote2;

		characterText.text = quotes[0];
		hand.gameObject.SetActive(true);
	}

	private void Quote2(Finger finger)
	{
		hand.gameObject.SetActive(false);
		Touch.onFingerDown -= Quote2;
		Touch.onFingerDown += Quote3;

		characterText.text = quotes[1];
	}

	private void Quote3(Finger finger)
	{
		player.Enabled = false;
		rail.Enabled = false;

		Touch.onFingerDown -= Quote3;
		fill.fillAmount = 0;
		currentScore = 0;
		player.transform.position = Vector2.zero;
		coinPool.Clear();
		spikesPool.Clear();
		rail.Clear();
		player.Clear();
		tutorialWindow.gameObject.SetActive(false);

		countingScreen.EndAction += OnCountEnd;
		countingScreen.Play();
	}

	private void OnCountEnd()
	{
		countingScreen.EndAction -= OnCountEnd;
		player.Enabled = true;
		rail.Enabled = true;
		player.Freeze(false);
		player.CoinCollected += OnPLayerCoinCollected;
		player.LoseAction += OnPlayerLose;
	}

	private void OnPLayerCoinCollected()
	{
		currentScore++;
		if (currentScore >= goal)
		{
			player.Enabled = false;
			rail.Enabled = false;
			player.CoinCollected -= OnPLayerCoinCollected;
			player.LoseAction -= OnPlayerLose;
			Quote4();
		}

		fill.fillAmount = (float)currentScore / (float)goal;
	}

	private void Quote4()
	{
		Touch.onFingerDown += Quote5;

		tutorialWindow.SetActive(true);
		characterText.text = quotes[2];
	}

	private void Quote5(Finger finger)
	{
		Touch.onFingerDown -= Quote5;
		Touch.onFingerDown += Quote6;

		characterText.text = quotes[3];
	}

	private void Quote6(Finger finger)
	{
		Touch.onFingerDown -= Quote6;

		LoadGame();
	}

	private void OnPlayerLose(ObstacleType obstacleType)
	{
		player.CoinCollected -= OnPLayerCoinCollected;
		player.LoseAction -= OnPlayerLose;
		player.Enabled = false;
		rail.Enabled = false;
		player.Freeze(true);

		tutorialWindow.SetActive(true);
		characterText.text = "don't worry, you will succeed if you try again!";
		Touch.onFingerDown += Quote3;
	}

	public void LoadGame()
	{
		SceneManager.LoadScene("Game");
	}
}
