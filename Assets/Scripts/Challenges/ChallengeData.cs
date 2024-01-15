using System;
using UnityEngine;

[Serializable]
public class ChallengeData
{
	public string description;
	public int goal;
	public RewardType rewardType;
	public int reward;
}

public enum RewardType
{
	Gem,
	Coin
}
