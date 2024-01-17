using System.Linq;
using UnityEngine;

public class BackgroundMusicController : MonoBehaviour
{
	[SerializeField] private AudioSource musicSource;
	[SerializeField] private SettingsScreen settingsScreen;

	private void Awake()
	{
		var musicControllers = GameObject.FindObjectsOfType<BackgroundMusicController>();
		var dontDestroyOnLoad = musicControllers.FirstOrDefault(x => x.gameObject.scene.name == "DontDestroyOnLoad");

		if (dontDestroyOnLoad != null && dontDestroyOnLoad != this)
		{
			Destroy(this.gameObject);
			return;
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
