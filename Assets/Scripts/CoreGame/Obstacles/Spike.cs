using Unity.VisualScripting;
using UnityEngine;


public class Spike : Poolable
{
	[SerializeField] private Material[] materials;
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private CircleCollider2D circleCollider2D;
	[SerializeField] private Vector2 rotationSpeedValues;

	private float rotationSpeed;
	public override Vector2 Size => spriteRenderer.size;

	private void Start()
	{
		rotationSpeed = Random.Range(rotationSpeedValues.x, rotationSpeedValues.y);
		var randomSize = Random.Range(sizeValues.x, sizeValues.y);

		circleCollider2D.radius = randomSize / 2;
		spriteRenderer.size = new Vector2(randomSize, randomSize);
	}

	public override void SpawnInitialize()
	{
		spriteRenderer.material = materials[Random.Range(0, materials.Length)];
	}

	private void Update()
	{
		var angles = transform.eulerAngles;
		angles.z += rotationSpeed * Time.deltaTime;
		transform.rotation = Quaternion.Euler(angles);
	}
}
