using System.Collections;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class RailController : MonoBehaviour
{
	[SerializeField] private PlayerMovement playerMovement;
	[SerializeField] private int railPieceCount;
	[SerializeField] private LineRenderer lineRenderer;
	[SerializeField] private float startRailEnd;
	[SerializeField] private AnimationCurve positionsMagnitudeRelative;
	[SerializeField] private float railSpeed;
	private Vector2 screenSize;
	public bool Enabled
	{
		get => isEnabled;
		set
		{
			isEnabled = value;
			if (value)
			{
				Touch.onFingerDown += OnFingerDown;
				Touch.onFingerUp += OnFingerUp;
				Touch.onFingerMove += OnFingerMove;
			}
			else
			{
				Touch.onFingerDown -= OnFingerDown;
				Touch.onFingerUp -= OnFingerUp;
				Touch.onFingerMove -= OnFingerMove;
			}
		}
	}

	private bool isEnabled;
	private Vector2 currentFingerStartPoint;
	private Vector2 currentMovingFingerPosition;
	private bool isShooting;
	private bool fingerDown;

	private void Start()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();

		screenSize = CameraSize.GetSize();
		SetRail();
	}

	private void OnFingerDown(Finger finger)
	{
		if (isShooting) return;
		if (fingerDown) return;
		fingerDown = true;

		currentFingerStartPoint = CameraSize.ScreenToWorldPoint(finger.screenPosition);
	}

	private void OnFingerUp(Finger finger)
	{
		if (isShooting) return;
		if (!fingerDown) return;

		var resultVector = currentMovingFingerPosition - currentFingerStartPoint;
		Debug.Log(resultVector.magnitude);
		if (resultVector.y > 0) return;

		isShooting = true;
		fingerDown = false;
		StartCoroutine(AddVertices(resultVector));
	}

	private void OnFingerMove(Finger finger)
	{
		if (isShooting) return;
		if (!fingerDown) return;

		currentMovingFingerPosition = CameraSize.ScreenToWorldPoint(finger.screenPosition);
	}

	private IEnumerator AddVertices(Vector2 magnitude)
	{
		int positionsCount = (int)positionsMagnitudeRelative.Evaluate(magnitude.magnitude);
		Vector2 step = magnitude / positionsCount;

		for (int i = 0; i < positionsCount; i++)
		{
			Vector2 startPosition = lineRenderer.GetPosition(lineRenderer.positionCount - 1);
			lineRenderer.positionCount++;
			startPosition += step;

			if (startPosition.x > screenSize.x - lineRenderer.startWidth)
			{
				step = Vector2.Reflect(step, Vector2.left);
			}

			if (startPosition.x < -screenSize.x + lineRenderer.startWidth)
			{
				step = Vector2.Reflect(step, Vector2.right);
			}

			lineRenderer.SetPosition(lineRenderer.positionCount - 1, startPosition);

			yield return new WaitForFixedUpdate();
		}

		isShooting = false;
	}

	public Vector2 GetClosestDirection(Vector2 point)
	{
		int closestVertexIndex = 0;
		float closestDistance = Mathf.Infinity;

		for (int i = 0; i < lineRenderer.positionCount; i++)
		{
			Vector3 linePoint = lineRenderer.GetPosition(i);
			float distance = Vector2.Distance(point, linePoint);

			if (distance < closestDistance)
			{
				closestDistance = distance;
				closestVertexIndex = i;
			}
		}

		if (closestVertexIndex == lineRenderer.positionCount - 1)
		{
			return Vector2.zero;
		}

		var result = lineRenderer.GetPosition(closestVertexIndex + 1) - lineRenderer.GetPosition(closestVertexIndex);

		return result;
	}

	public void Clear()
	{
		SetRail();
		isShooting = false;
		fingerDown = false;
		currentFingerStartPoint = Vector2.zero;
		currentMovingFingerPosition = Vector2.zero;
	}

	private void SetRail()
	{
		lineRenderer.positionCount = railPieceCount;

		var startPosition = playerMovement.SpawnPosition;
		Vector3[] positions = new Vector3[railPieceCount];
		float destination = startPosition.y - 2 * screenSize.y * startRailEnd;
		float distance = startPosition.y - destination;
		float step = Mathf.Abs(distance / railPieceCount);

		for (int i = 0; i < railPieceCount; i++)
		{
			positions[i] = startPosition;
			startPosition.y -= step;
		}

		lineRenderer.SetPositions(positions);
	}

	public Vector2 RailNormal(float yPosition)
	{
		return Vector2.zero;
	}

	private void OnDestroy()
	{
		Touch.onFingerDown -= OnFingerDown;
		Touch.onFingerUp -= OnFingerUp;
		Touch.onFingerMove -= OnFingerMove;
	}
}
