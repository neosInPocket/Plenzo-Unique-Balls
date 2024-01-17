using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
	public void LoadGame()
	{
		if (SavingCore.Data.enteredGame)
		{
			SavingCore.Data.enteredGame = false;
			SavingCore.SaveData();
			SceneManager.LoadScene("Tutorial");
		}
		else
		{
			SceneManager.LoadScene("Game");
		}
	}
}
