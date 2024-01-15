using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopRefresher : MonoBehaviour
{
	[SerializeField] private UpgradeCardViewer card;
	[SerializeField] private GoodsRefreshController goodsRefreshController;
	[SerializeField] private Button healthViewButton;
	[SerializeField] private Button speedViewButton;
	[SerializeField] private Image goodImage;
	[SerializeField] private TMP_Text costAmount;
	[SerializeField] private Sprite coinIcon;
	[SerializeField] private Sprite gemIcon;
	private bool isHealthShowing;

	private void Start()
	{
		Refresh();
	}

	public void Refresh()
	{
		goodsRefreshController.RefreshInfo();
		RefreshSpeed();
		RefreshHealth();
	}

	public void ViewHealth()
	{
		isHealthShowing = true;
		goodImage.sprite = gemIcon;
		costAmount.text = 1.ToString();

		card.gameObject.SetActive(true);
		RefreshHealth();
	}

	public void ViewSpeed()
	{
		isHealthShowing = false;
		goodImage.sprite = coinIcon;
		costAmount.text = 60.ToString();

		card.gameObject.SetActive(true);
		RefreshSpeed();
	}

	public void RefreshHealth()
	{
		card.ShowProgress(SavingCore.Data.healthUpgrades);

		if (SavingCore.Data.gems >= 1 && SavingCore.Data.healthUpgrades < 3)
		{
			card.Status = "HEALTH UPGRADE";
			card.Interactable = true;
			card.StatusTextColor = Color.white;
		}
		else
		{
			if (SavingCore.Data.healthUpgrades >= 3)
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

		if (SavingCore.Data.coins >= 60 && SavingCore.Data.healthUpgrades < 3)
		{
			card.Status = "HEALTH UPGRADE";
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

	private void PurchaseHealth()
	{
		SavingCore.Data.gems--;
		SavingCore.Data.healthUpgrades++;
		SavingCore.SaveData();
		Refresh();
	}

	private void PurchaseSpeed()
	{
		SavingCore.Data.coins -= 60;
		SavingCore.Data.speedUpgrades++;
		SavingCore.SaveData();
		Refresh();
	}

	public void Purchase()
	{
		if (isHealthShowing)
		{
			PurchaseHealth();
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
