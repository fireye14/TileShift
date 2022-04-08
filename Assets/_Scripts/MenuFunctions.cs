using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuFunctions : MonoBehaviour {

	public GameObject fader;
    public GameObject hintButton;

	public void MenuShowAndHide()
	{
		gameObject.SetActive(!gameObject.activeSelf);
		fader.SetActive(!fader.activeSelf);
        hintButton.SetActive(!hintButton.activeSelf);
	}

	public void RestartLevel()
	{
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void ExitLevel()
	{
		string toLoad = GameData.random ? "ModeSelect" : "LevelSelect";
        SceneManager.LoadScene(toLoad);
	}

	public void GoHome()
	{
        SceneManager.LoadScene("StartScreen");
	}

}
