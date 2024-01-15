using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Challenges")]
public class PossibleChallenges : ScriptableObject
{
	[SerializeField] private List<ChallengeData> challengeDatas;
	public List<ChallengeData> ChallengeDatas => challengeDatas;
}
