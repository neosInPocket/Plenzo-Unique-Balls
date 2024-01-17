using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class CountingScreen : MonoBehaviour
{
	[SerializeField] private TMP_Text mainText;
	[SerializeField] private string[] countQuotes;
	[SerializeField] private float quoteDelay;
	public Action EndAction { get; set; }
	public Action OnShowQuoteEnd { get; set; }

	public void Play()
	{
		StartCoroutine(Count());
	}

	public void ShowQuote(string text)
	{
		StopAllCoroutines();
		mainText.gameObject.SetActive(false);
		StartCoroutine(Quote(text));
	}

	private IEnumerator Quote(string caption)
	{
		mainText.text = caption;
		mainText.gameObject.SetActive(true);
		yield return new WaitForSeconds(quoteDelay);
		mainText.gameObject.SetActive(false);
		OnShowQuoteEnd?.Invoke();
	}

	private IEnumerator Count()
	{
		for (int i = 0; i < countQuotes.Length; i++)
		{
			mainText.text = countQuotes[i];
			mainText.gameObject.SetActive(true);
			yield return new WaitForSeconds(quoteDelay);
			mainText.gameObject.SetActive(false);
		}

		EndAction?.Invoke();
	}
}
