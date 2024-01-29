using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private Vector2 spawnPosition;
	[SerializeField] private RailController railController;
	private float verticalSpeed;
	[SerializeField] private Rigidbody2D rb;
	[SerializeField] private GameObject loseEffect;
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private TrailRenderer trailRenderer;
	public Vector2 SpawnPosition => spawnPosition;
	public Action<ObstacleType> LoseAction { get; set; }
	public Action CoinCollected { get; set; }
	private ChallengeData timeChallengeData;
	private ChallengeData distanceChallengeData;

	public bool Enabled
	{
		get => isEnabled;
		set
		{
			if (!value)
			{
				rb.velocity = Vector2.zero;
			}

			isEnabled = value;
		}
	}

	private bool isEnabled;
	private bool ranOff;
	private Vector2 screenSize;

	private void Start()
	{
		timeChallengeData = SavingCore.Data.currentChallenges.FirstOrDefault(x => x.challengeType == ChallengeType.SlideTime);
		distanceChallengeData = SavingCore.Data.currentChallenges.FirstOrDefault(x => x.challengeType == ChallengeType.Distance);

		transform.position = spawnPosition;
		screenSize = CameraSize.GetSize();

		switch (SavingCore.Data.speedUpgrades)
		{
			case 0:
				verticalSpeed = 2f;
				break;

			case 1:
				verticalSpeed = 1.85f;
				break;

			case 2:
				verticalSpeed = 1.76f;
				break;

			case 3:
				verticalSpeed = 1.5f;
				break;
		}
	}

	private void Update()
	{
		if (isEnabled == false) return;

		if (!timeChallengeData.Completed)
		{
			timeChallengeData.progress += Time.deltaTime;
		}

		if (!distanceChallengeData.Completed)
		{
			distanceChallengeData.progress += Time.deltaTime * verticalSpeed;
		}

		if (transform.position.x > screenSize.x || transform.position.x < -screenSize.x)
		{
			Destroy(ObstacleType.Slipped);
			return;
		}

		if (!isEnabled || ranOff) return;

		var closestDirection = railController.GetClosestDirection(transform.position);
		if (closestDirection == Vector2.zero)
		{
			ranOff = true;
			railController.Enabled = false;
			rb.gravityScale = 1;
			return;
		}

		var factor = Mathf.Abs(verticalSpeed / closestDirection.normalized.y);


		rb.velocity = closestDirection.normalized * factor * verticalSpeed;
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.TryGetComponent<Coin>(out Coin coin))
		{
			coin.Collect();
			CoinCollected?.Invoke();
			return;
		}

		if (collider.TryGetComponent<Spike>(out Spike spike))
		{
			Destroy(ObstacleType.Saw);
			return;
		}
	}

	private void Destroy(ObstacleType obstacleType)
	{
		LoseAction?.Invoke(obstacleType);
		StartCoroutine(DestroyRoutine());
		Enabled = false;
		trailRenderer.time = 1f;
		rb.constraints = RigidbodyConstraints2D.FreezeAll;
	}

	private IEnumerator DestroyRoutine()
	{
		spriteRenderer.enabled = false;
		loseEffect.SetActive(true);
		yield return new WaitForSeconds(1f);
		loseEffect.SetActive(false);
	}

	public void Clear()
	{
		spriteRenderer.enabled = true;
		trailRenderer.time = 1.78f;
		ranOff = false;
	}

	public void Freeze(bool value)
	{
		if (value)
		{
			rb.constraints = RigidbodyConstraints2D.FreezeAll;
			trailRenderer.enabled = false;
		}
		else
		{
			rb.constraints = RigidbodyConstraints2D.None;
			trailRenderer.Clear();
			trailRenderer.enabled = true;
		}

	}

	private void OnDestroy()
	{
		SavingCore.SaveData();
	}
}

public enum ObstacleType
{
	Saw,
	Slipped
}
