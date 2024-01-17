using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelHolder : MonoBehaviour
{
	[SerializeField] private PlayerMovement player;
	[SerializeField] private RailController railController;
	[SerializeField] private CountingScreen countingScreen;
	[SerializeField] private PauseController pauseController;
	[SerializeField] private TMP_Text levelValue;
	[SerializeField] private Image fillImage;
	[SerializeField] private int scorePerCoin;
	[SerializeField] private EndScreen endScreen;
	private int currentScore = 0;
	private int maxScore => (int)(10 * Mathf.Log(Mathf.Sqrt(SavingCore.Data.gameLevel) + 2));
	private int reward => (int)(10 * Mathf.Log(Mathf.Pow(SavingCore.Data.gameLevel, 2) + 2) + 12);
	private ChallengeData coinChallenge;

	private void Start()
	{
		coinChallenge = SavingCore.Data.currentChallenges.FirstOrDefault(x => x.challengeType == ChallengeType.CoinCollect);

		countingScreen.EndAction += OnCountEnd;
		player.CoinCollected += PlayerCoinCollected;
		player.LoseAction += PlayerSlipped;

		countingScreen.Play();
		levelValue.text = $"LEVEL {SavingCore.Data.gameLevel}";
		fillImage.fillAmount = 0f;
	}

	private void OnCountEnd()
	{
		countingScreen.EndAction -= OnCountEnd;
		player.Enabled = true;
		railController.Enabled = true;
		pauseController.Enabled = true;
	}

	private void PlayerCoinCollected()
	{
		if (!coinChallenge.Completed)
		{
			coinChallenge.progress++;
		}

		countingScreen.ShowQuote("COIN!");
		currentScore += scorePerCoin;
		CheckCurrentScore();
	}

	private void CheckCurrentScore()
	{
		if (currentScore >= maxScore)
		{
			currentScore = maxScore;
			Win();
		}

		fillImage.fillAmount = (float)currentScore / (float)maxScore;
	}

	private void Win()
	{
		endScreen.Show("YOU WIN!", "NEXT LEVEL", reward, 1);
		SavingCore.Data.gameLevel++;
		SavingCore.Data.coins += reward;
		SavingCore.Data.gems++;
		SavingCore.SaveData();

		player.Enabled = false;
		railController.Enabled = false;
		pauseController.Enabled = false;
	}

	private void Lose()
	{
		countingScreen.OnShowQuoteEnd -= Lose;
		endScreen.Show("YOU LOSE", "TRY AGAIN");
	}

	private void PlayerSlipped(ObstacleType obstacleType)
	{
		if (obstacleType == ObstacleType.Saw)
		{
			countingScreen.OnShowQuoteEnd += Lose;
			countingScreen.ShowQuote("SAW!");
		}
		else
		{
			countingScreen.OnShowQuoteEnd += Lose;
			countingScreen.ShowQuote("SLIPPED..");
		}
		player.CoinCollected -= PlayerCoinCollected;
		player.LoseAction -= PlayerSlipped;

		player.Enabled = false;
		railController.Enabled = false;
		pauseController.Enabled = false;
	}

	private void OnDestroy()
	{
		countingScreen.EndAction -= OnCountEnd;
		player.CoinCollected -= PlayerCoinCollected;
		player.LoseAction -= PlayerSlipped;
		SavingCore.SaveData();
	}

	public void TryAgain()
	{
		SceneManager.LoadScene("Game");
	}

	public void Menu()
	{

		SceneManager.LoadScene("Menu");
	}
}
