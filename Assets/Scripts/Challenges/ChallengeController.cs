using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShortcutManagement;
using UnityEngine;

public class ChallengeController : MonoBehaviour
{
	[SerializeField] private PossibleChallenges possibleChallenges;
	[SerializeField] private ChallengeWindow[] challenges;
	[SerializeField] private Sprite gemSprite;
	[SerializeField] private Sprite coinSprite;

	private void Start()
	{
		if (SavingCore.Data.currentChallenges == null)
		{
			SetChallenges();
		}
	}

	private void SetChallenges()
	{
		SavingCore.Data.currentChallenges = new List<ChallengeData>();

		for (int i = 0; i < challenges.Length; i++)
		{
			var newChallenge = possibleChallenges.ChallengeDatas[i];
			challenges[i].Caption = newChallenge.description;
			challenges[i].Reward = newChallenge.reward;
			challenges[i].Progress = 0f;
			challenges[i].CurrentChallenge = newChallenge;

			SavingCore.Data.currentChallenges.Add(newChallenge);
			SavingCore.SaveData();
		}
	}
}
