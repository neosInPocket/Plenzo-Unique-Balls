using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObstaclesPool : MonoBehaviour
{
	[SerializeField] private Poolable prefab;
	[SerializeField] private int initialSize;
	private List<Poolable> currentObjects;

	private void Start()
	{
		currentObjects = new List<Poolable>();
		CreateInitialPool();
	}

	private void CreateInitialPool()
	{
		for (int i = 0; i < initialSize; i++)
		{
			var obj = Instantiate(prefab, transform);
			obj.gameObject.SetActive(false);
			currentObjects.Add(obj);
		}
	}

	public Poolable Pool(Vector2 position)
	{
		Poolable freeObject = currentObjects.FirstOrDefault(x => !x.gameObject.activeSelf);

		if (freeObject != null)
		{
			freeObject.transform.position = position;
			freeObject.gameObject.SetActive(true);
			freeObject.Initialize();
			return freeObject;
		}
		else
		{
			var obj = Instantiate(prefab, transform);
			currentObjects.Add(obj);
			obj.transform.position = position;
			obj.gameObject.SetActive(true);
			obj.Initialize();
			return obj;
		}
	}

	public void Clear()
	{
		foreach (var obstacle in currentObjects)
		{
			obstacle.gameObject.SetActive(false);
		}
	}

	public Poolable Pool() => Pool(Vector2.zero);
}
