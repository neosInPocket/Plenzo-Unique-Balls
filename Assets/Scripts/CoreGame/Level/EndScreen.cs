using TMPro;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
	[SerializeField] private TMP_Text resultCaption;
	[SerializeField] private TMP_Text coinsText;
	[SerializeField] private TMP_Text gemsText;
	[SerializeField] private TMP_Text buttonText;

	public void Show(string caption, string buttonCaption, int coins = 0, int gems = 0)
	{
		gameObject.SetActive(true);
		resultCaption.text = caption;
		gemsText.text = gems.ToString();
		coinsText.text = coins.ToString();
		buttonText.text = buttonCaption;
	}
}
