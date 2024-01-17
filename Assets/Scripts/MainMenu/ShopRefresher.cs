using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopRefresher : MonoBehaviour
{
	[SerializeField] private UpgradeCardViewer card;
	[SerializeField] private GoodsRefreshController goodsRefreshController;
	[SerializeField] private Button coinChanceViewButton;
	[SerializeField] private Button speedViewButton;
	[SerializeField] private Image goodImage;
	[SerializeField] private TMP_Text costAmount;
	[SerializeField] private Sprite coinIcon;
	[SerializeField] private Sprite gemIcon;
	private bool isHealthShowing;

	public void ViewCoinChance()
	{
		isHealthShowing = true;
		goodImage.sprite = gemIcon;
		costAmount.text = 1.ToString();

		card.gameObject.SetActive(true);
		RefreshCoinChance();
	}

	public void ViewSpeed()
	{
		isHealthShowing = false;
		goodImage.sprite = coinIcon;
		costAmount.text = 60.ToString();

		card.gameObject.SetActive(true);
		RefreshSpeed();
	}

	public void RefreshCoinChance()
	{
		card.ShowProgress(SavingCore.Data.coinSpawnChance);

		if (SavingCore.Data.gems >= 1 && SavingCore.Data.coinSpawnChance < 3)
		{
			card.Status = "COIN SPAWN CHANCE";
			card.Interactable = true;
			card.StatusTextColor = Color.white;
		}
		else
		{
			if (SavingCore.Data.coinSpawnChance >= 3)
			{
				card.Status = "UPGRADED TO MAX";
				card.StatusTextColor = Color.green;
				card.Interactable = false;
				return;
			}

			if (SavingCore.Data.gems < 1)
			{
				card.Status = "NOT ENOUGH GEMS";
				card.StatusTextColor = Color.red;
				card.Interactable = false;
				return;
			}
		}
	}

	public void RefreshSpeed()
	{
		card.ShowProgress(SavingCore.Data.speedUpgrades);

		if (SavingCore.Data.coins >= 60 && SavingCore.Data.speedUpgrades < 3)
		{
			card.Status = "BALL SPEED";
			card.Interactable = true;
			card.StatusTextColor = Color.white;
		}
		else
		{
			if (SavingCore.Data.speedUpgrades >= 3)
			{
				card.Status = "UPGRADED TO MAX";
				card.StatusTextColor = Color.green;
				card.Interactable = false;
				return;
			}

			if (SavingCore.Data.coins < 60)
			{
				card.Status = "NOT ENOUGH COINS";
				card.Interactable = false;
				card.StatusTextColor = Color.red;

				return;
			}
		}
	}

	private void PurchaseCoinChance()
	{
		SavingCore.Data.gems--;
		SavingCore.Data.coinSpawnChance++;
		SavingCore.SaveData();
		goodsRefreshController.RefreshInfo();
		RefreshCoinChance();

	}

	private void PurchaseSpeed()
	{
		SavingCore.Data.coins -= 60;
		SavingCore.Data.speedUpgrades++;
		SavingCore.SaveData();
		goodsRefreshController.RefreshInfo();
		RefreshSpeed();
	}

	public void Purchase()
	{
		if (isHealthShowing)
		{
			PurchaseCoinChance();
		}
		else
		{
			PurchaseSpeed();
		}
	}

	public void CloseCard()
	{
		card.gameObject.SetActive(false);
	}
}
