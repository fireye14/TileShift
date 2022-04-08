using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class ModeSelect : MonoBehaviour {
	

	public void LoadLevelSelect(string prefix)
	{
        SceneManager.LoadScene(prefix + "LevelSelect");
	}

	public void LoadRandom(string prefix)
	{
        SceneManager.LoadScene(prefix + "Random");
	}
}
