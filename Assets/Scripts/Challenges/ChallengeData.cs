using System;
using Unity.VisualScripting;

[Serializable]
public class ChallengeData
{
	public ChallengeType challengeType;
	public string description;
	public int goal;
	public RewardType rewardType;
	public int reward;
	public float progress;
	public bool checkedCompleted;
	public bool Completed => progress >= goal;
}

public enum RewardType
{
	Gem,
	Coin
}

public enum ChallengeType
{
	CoinCollect,
	Distance,
	SlideTime
}
