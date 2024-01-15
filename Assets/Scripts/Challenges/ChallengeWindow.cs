using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeWindow : MonoBehaviour
{
	[SerializeField] private TMP_Text caption;
	[SerializeField] private Image progressFill;
	[SerializeField] private Image goodImage;
	public string Caption
	{
		get => caption.text;
		set => caption.text = value;
	}

	public int Reward
	{
		get => (int)caption.text.ConvertTo(typeof(string));
		set => caption.text = value.ToString();
	}

	public float Progress
	{
		get => progressFill.fillAmount;
		set => progressFill.fillAmount = value;
	}

	public Sprite Icon
	{
		get => goodImage.sprite;
		set => goodImage.sprite = value;
	}

	public ChallengeData CurrentChallenge { get; set; }
}
