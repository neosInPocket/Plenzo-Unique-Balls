using UnityEngine;

public class Spawner : MonoBehaviour
{
	[SerializeField] private ObstaclesPool obstaclesPool;
	[SerializeField] private float cameraSpawnOffset;
	[SerializeField] private Vector2 spawnOffsetValues;
	[SerializeField] private Poolable lastObject;
	private Poolable currentLastObject;
	private Vector2 screenSize;
	private float cameraTopBound => Camera.main.transform.position.y + screenSize.y;
	private float cameraBottomBound => Camera.main.transform.position.y - screenSize.y;

	private void Start()
	{
		currentLastObject = lastObject;
		screenSize = CameraSize.GetSize();
	}

	private void Update()
	{
		if (cameraBottomBound - cameraSpawnOffset < currentLastObject.transform.position.y)
		{
			var randomX = Random.Range(-screenSize.x * 4 / 5, screenSize.x * 4 / 5);
			var position = new Vector2(randomX, currentLastObject.transform.position.y - Random.Range(spawnOffsetValues.x, spawnOffsetValues.y));
			var newObject = obstaclesPool.Pool(position);
			currentLastObject = newObject;
		}
	}

	public void Clear()
	{
		currentLastObject = lastObject;
		obstaclesPool.Clear();
		lastObject.GetComponent<SpriteRenderer>().enabled = true;
	}
}
