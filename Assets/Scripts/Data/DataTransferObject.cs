using System;
using System.Collections.Generic;
[Serializable]
public class DataTransferObject
{
	public int coins = 100;
	public int gems = 1;
	public int coinSpawnChance = 0;
	public int speedUpgrades = 0;
	public bool enteredGame = true;

	public int gameLevel = 1;
	public int firstUpgrade = 0;
	public int secondUpgrade = 0;
	public float volume = 1f;
	public float sfx = 1f;
	public List<ChallengeData> currentChallenges;
}
