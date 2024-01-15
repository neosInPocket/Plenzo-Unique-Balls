using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCardViewer : MonoBehaviour
{
	[SerializeField] private List<Image> checkmarks;
	[SerializeField] private TMP_Text statusCaption;
	[SerializeField] private Button purchaseButton;

	public bool Interactable
	{
		get => purchaseButton.interactable;
		set => purchaseButton.interactable = value;
	}

	public string Status
	{
		get => statusCaption.text;
		set => statusCaption.text = value;
	}

	public Color StatusTextColor
	{
		get => statusCaption.color;
		set => statusCaption.color = value;
	}


	public void ShowProgress(int progress)
	{
		checkmarks.ForEach(x => x.enabled = false);

		for (int i = 0; i < progress; i++)
		{
			checkmarks[i].enabled = true;
		}
	}
}
