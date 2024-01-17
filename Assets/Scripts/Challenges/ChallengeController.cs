using System.Collections.Generic;
using UnityEngine;

public class ChallengeController : MonoBehaviour
{
	[SerializeField] private PossibleChallenges possibleChallenges;
	[SerializeField] private ChallengeWindow[] challenges;
	[SerializeField] private Sprite gemSprite;
	[SerializeField] private Sprite coinSprite;
	[SerializeField] float alpha;

	private void Start()
	{
		if (SavingCore.Data.currentChallenges == null)
		{
			SetChallenges();
		}
		else
		{
			if (SavingCore.Data.currentChallenges.Count == 0)
			{
				SetChallenges();
			}
		}

		Refresh();
	}

	private void SetChallenges()
	{
		SavingCore.Data.currentChallenges = new List<ChallengeData>();
		SavingCore.Data.currentChallenges.AddRange(possibleChallenges.ChallengeDatas);
		SavingCore.SaveData();
	}

	private void Refresh()
	{
		var currentChallenges = SavingCore.Data.currentChallenges;

		for (int i = 0; i < challenges.Length; i++)
		{
			challenges[i].Caption = currentChallenges[i].description;
			challenges[i].Reward = currentChallenges[i].reward;
			challenges[i].Progress = (float)currentChallenges[i].progress / (float)currentChallenges[i].goal;
			challenges[i].CurrentChallenge = currentChallenges[i];

			if (currentChallenges[i].rewardType == RewardType.Gem)
			{
				challenges[i].Icon = gemSprite;
			}
			else
			{
				challenges[i].Icon = coinSprite;
			}

			if (currentChallenges[i].Completed)
			{
				challenges[i].CanvasGroup.alpha = alpha;

				if (!currentChallenges[i].checkedCompleted)
				{
					currentChallenges[i].checkedCompleted = true;
					if (currentChallenges[i].rewardType == RewardType.Gem)
					{
						SavingCore.Data.gems += currentChallenges[i].reward;
					}
					else
					{
						SavingCore.Data.coins += currentChallenges[i].reward;
					}

				}
			}
		}
	}

	private void CompleteChallenge()
	{

	}
}
