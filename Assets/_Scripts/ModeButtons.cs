using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ModeButtons : MonoBehaviour {

    public Text SubModeText;

    private void Awake()
    {
        SubModeText.text = GameData.random ? "Random" : "Normal";
    }

    public void LoadLevelSelect(string table)
	{
		GameData.tableName = table;
		GameData.random = false;
		
		//if the last character in tableName is an underscore, only use the 2nd to last character of the string as numStates; otherwise use the last two
		//ex. table = 3_2_ -> numStates = 2
		//ex. table = 3_11 -> numStates = 11
		GameData.numStates = table.Substring(table.Length - 1) == "_" ? int.Parse(table.Substring(table.Length - 2, 1)) : int.Parse(table.Substring(table.Length - 2, 2));

        //Application.LoadLevel("LevelSelect");
        StartCoroutine(LoadNewScene("LevelSelect"));
	}
	
	public void LoadRandom(string table)
	{
		GameData.tableName = table.Substring(table.Length - 6);
		GameData.random = true;
		GameData.numStates = table.Substring(3, 1) == "_" ? int.Parse (table.Substring(2, 1)) : int.Parse(table.Substring(2, 2));
        SceneManager.LoadScene("Game_3");
	}

	public void GoBack()
	{
        SceneManager.LoadScene("StartScreen");
	}

    IEnumerator LoadNewScene(string scene)
    {
        // wait until level assets are done loading before loading the scene
        AsyncOperation async = SceneManager.LoadSceneAsync(scene);

        while (!async.isDone)
        {
            yield return null;
        }
    }

}
