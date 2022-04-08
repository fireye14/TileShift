using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour {

	public Button[] levelButtons;
	public Text musicButtonText;
	private AudioSource audioSource;
   
    private Color[] stateColors = new Color[] {
		new Color(208/255f, 208/255f, 208/255f),   		//state zero = white
		new Color(0.0f, 214/255f, 214/255f),			//state one = blue
		new Color(1f, 68/255f, 68/255f, 1.0f),			//state two = red
		new Color(244/255f, 221/255f, 51/255f),			//state three = yellow
		new Color(153/255f, 94/255f, 1.0f),				//state four = purple
		new Color(0f, 214/255f, 110/255f),				//state five = green
		new Color(248/255f, 87/255f, 0f),				//state six = orange
		new Color(253/255f, 143/255f, 235/255f),		//state seven = pink
		new Color(0f, 64/255f, 189/255f),				//state eight = dark blue
		new Color(116/255f, 26/255f, 0f),				//state nine = burgundy
		new Color(30/255f, 30/255f, 30/255f)};			//state ten = black	

	public void StartLevel(int levelNum)
	{
		GameData.levelNum = levelNum;
        SceneManager.LoadScene("Game_3");
	}

	public void PlayGame(bool random)
	{
        GameData.random = random;
        SceneManager.LoadScene("ModeSelect");
	}

	public void HowToPlay()
	{
        SceneManager.LoadScene("HowToPlay");
	}

	public void ToggleMusic()
	{
		if(audioSource == null) audioSource = GameObject.Find("Music").GetComponent<AudioSource>();
		audioSource.enabled = !audioSource.enabled;
		musicButtonText.text = "Music: " + (audioSource.enabled ? "On" : "Off");

        //Update music setting in database
        GameData.UpdateSettings("music", audioSource.enabled);

    }

}
