using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScreen : MonoBehaviour
{
	[SerializeField] private Slider musicSlider;
	[SerializeField] private Slider sfxSlider;
	public BackgroundMusicController Controller { get; set; }

	private void Start()
	{
		Controller = FindObjectsOfType<BackgroundMusicController>().FirstOrDefault();
		RefershSliders();
	}

	private void RefershSliders()
	{
		musicSlider.value = SavingCore.Data.volume;
		sfxSlider.value = SavingCore.Data.sfx;
	}

	public void SetMusicVolume(float value)
	{
		SavingCore.Data.volume = value;
		Controller.ChangeMusicVolume(value);
	}

	public void SetSFXVolume(float value)
	{
		SavingCore.Data.sfx = value;
	}

	public void SaveVolume()
	{
		SavingCore.SaveData();
	}
}
