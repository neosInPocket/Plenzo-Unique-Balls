using System.Linq;
using UnityEngine;

public class BackgroundMusicController : MonoBehaviour
{
	[SerializeField] private AudioSource musicSource;
	[SerializeField] private SettingsScreen settingsScreen;

	private void Awake()
	{
		var musicControllers = GameObject.FindObjectsOfType<BackgroundMusicController>();
		var nonMine = musicControllers.FirstOrDefault(x => x != this);

		if (nonMine != null && musicControllers.Length > 1)
		{
			if (nonMine.gameObject.scene.name != "DontDestroyOnLoad")
			{
				Destroy(nonMine.gameObject);
			}
		}
		else
		{
			DontDestroyOnLoad(gameObject);
		}

		settingsScreen.Controller = this;
	}

	private void Start()
	{
		ChangeMusicVolume(SavingCore.Data.volume);
	}

	public void ChangeMusicVolume(float value)
	{
		musicSource.volume = value;
	}
}
