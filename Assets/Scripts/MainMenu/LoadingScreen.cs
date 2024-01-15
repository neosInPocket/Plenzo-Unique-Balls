using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
	[SerializeField] private Material loadingMaterial;
	[SerializeField] private string valueString;
	[SerializeField] private Vector2 minMax;
	[SerializeField] private float speed;
	[SerializeField] private List<Transform> allScreens;
	[SerializeField] private CanvasGroup canvasGroup;
	[SerializeField] private UpgradeCardViewer upgradeCardViewer;
	private Transform currentScreen;

	private void Start()
	{
		loadingMaterial.SetFloat(valueString, minMax.x);
		currentScreen = allScreens.FirstOrDefault(x => x.gameObject.activeSelf);
	}

	public void LoadScreen(Transform screen)
	{
		if (currentScreen == screen) return;
		canvasGroup.blocksRaycasts = true;
		StartCoroutine(Fade(screen, 1f));
	}

	private IEnumerator Fade(Transform screen, float direction)
	{
		var destination = direction > 0 ? minMax.y : minMax.x;
		var currentLoadValue = loadingMaterial.GetFloat(valueString);

		while ((currentLoadValue < destination && direction > 0) || (currentLoadValue > destination && direction < 0))
		{
			currentLoadValue += direction * speed * Time.deltaTime;
			loadingMaterial.SetFloat(valueString, currentLoadValue);
			yield return null;
		}

		loadingMaterial.SetFloat(valueString, destination);
		upgradeCardViewer.gameObject.SetActive(false);
		currentScreen.gameObject.SetActive(false);
		screen.gameObject.SetActive(true);
		currentScreen = screen;

		direction *= -1;
		destination = direction > 0 ? minMax.y : minMax.x;

		while ((currentLoadValue < destination && direction > 0) || (currentLoadValue > destination && direction < 0))
		{
			currentLoadValue += direction * speed * Time.deltaTime;
			loadingMaterial.SetFloat(valueString, currentLoadValue);
			yield return null;
		}

		loadingMaterial.SetFloat(valueString, destination);
		canvasGroup.blocksRaycasts = false;
	}
}
