using UnityEngine;

public abstract class Poolable : MonoBehaviour
{
	[SerializeField] protected Vector2 sizeValues;
	public abstract Vector2 Size { get; }
	private Vector2 screenSize;

	private void Awake()
	{
		screenSize = CameraSize.GetSize();
	}

	public void Initialize()
	{
		var raycastHit = Physics2D.Raycast(transform.position, Vector3.forward);

		if (raycastHit.collider != null)
		{
			if (raycastHit.collider.TryGetComponent<Poolable>(out Poolable poolable) && poolable.gameObject.activeSelf)
			{
				Vector2 position = transform.position;

				if (Mathf.Abs(poolable.transform.position.x - poolable.Size.x / 2 - screenSize.x) < Size.x)
				{
					position.x = Random.Range(poolable.transform.position.x + poolable.Size.x / 2 + Size.x / 2, screenSize.x - Size.x / 2);
				}
				else
				{
					position.x = Random.Range(screenSize.x + Size.x / 2, poolable.transform.position.x - poolable.Size.x / 2 - Size.x / 2);
				}

				if (Mathf.Abs(screenSize.x - poolable.transform.position.x + poolable.Size.x / 2) < Size.x)
				{
					position.x = Random.Range(screenSize.x + Size.x / 2, poolable.transform.position.x - poolable.Size.x / 2 - Size.x / 2);
				}
				else
				{
					position.x = Random.Range(poolable.transform.position.x + poolable.Size.x / 2 + Size.x / 2, screenSize.x - Size.x / 2);
				}


			}
		}

		SpawnInitialize();
	}

	public abstract void SpawnInitialize();

}
