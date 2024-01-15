using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoodsRefreshController : MonoBehaviour
{
	[SerializeField] private TMP_Text coins;
	[SerializeField] private TMP_Text gems;

	private void Start()
	{
		RefreshInfo();
	}

	public void RefreshInfo()
	{
		coins.text = SavingCore.Data.coins.ToString();
		gems.text = SavingCore.Data.gems.ToString();
	}
}
