using System.Collections;
using UnityEngine;

public class Coin : Poolable
{
	[SerializeField] private GameObject collectEffect;
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private GameObject glowEffect;
	private float spawnChance;
	private bool collected;
	public bool Collected => collected;

	public override Vector2 Size => spriteRenderer.size;

	public void Collect()
	{
		collected = true;
		StartCoroutine(CollectEffect());
	}

	public override void SpawnInitialize()
	{
		spawnChance = SpawnChance();

		if (spawnChance > Random.Range(0, 1f))
		{
			gameObject.SetActive(true);
		}
		else
		{
			gameObject.SetActive(false);
		}

		spriteRenderer.enabled = true;
		glowEffect.SetActive(true);
		collected = false;
	}

	private IEnumerator CollectEffect()
	{
		collectEffect.SetActive(true);
		glowEffect.SetActive(false);
		spriteRenderer.enabled = false;
		yield return new WaitForSeconds(1f);
		collectEffect.SetActive(false);
	}

	private float SpawnChance()
	{
		switch (SavingCore.Data.coinSpawnChance)
		{
			case 0: return 0.5f;
			case 1: return 0.6f;
			case 2: return 0.7f;
			case 3: return 0.8f;
			default: return 1f;
		}
	}
}
