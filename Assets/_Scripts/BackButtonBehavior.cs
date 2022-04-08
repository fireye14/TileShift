using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackButtonBehavior : MonoBehaviour {

	public static BackButtonBehavior behavior;

	public GameObject menu;
	public GameObject fader;

    private int frameCount;

	// Use this for initialization
	void Awake() 
	{
		if(behavior == null)
		{
            //if behavior hasn't been set yet, set it to this instance, and don't destroy it
            DontDestroyOnLoad(gameObject);
			behavior = this;
		}
		else if(behavior != this)
		{
            //if we just got to Game_3 scene, steal the fader and menu objects before destroying
            if (SceneManager.GetActiveScene().name == "Game_3")                
            {
                behavior.fader = this.fader;
                behavior.menu = this.menu;
            }
            Destroy(gameObject);
        }
	}
	
	// Update is called once per frame
	void Update() 
	{

		if(Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
		{

            // For some reason on my Android device, if you hold down the Back button, during a scene change, another KeyDown event will be sent about 20-30 frames later
            if (++frameCount >= 30 && Input.GetKeyDown(KeyCode.Escape))
			{
               
                frameCount = 0;

                switch (SceneManager.GetActiveScene().name)                    
                {
				    case "ModeSelect":
                        SceneManager.LoadScene("StartScreen");
					    break;

				    case "LevelSelect":
                        SceneManager.LoadScene("ModeSelect");
					    break;

                    case "StartScreen":
                        //Application.Quit();
                        if (Application.platform == RuntimePlatform.Android)
                        {
                            AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
                            activity.Call<bool>("moveTaskToBack", true);
                        }
                        break;

                    case "Game_3":
						if(fader != null) fader.SetActive(!fader.activeSelf);
						if(menu != null) menu.SetActive(!menu.activeSelf);
					    break;

					case "HowToPlay":
                        SceneManager.LoadScene("StartScreen");
						break;
				}
					
			}
		}
	}
}
