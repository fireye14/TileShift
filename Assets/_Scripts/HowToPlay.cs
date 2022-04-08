using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.SceneManagement;

public class HowToPlay : MonoBehaviour {

    public GameObject screenFader;
    public GameObject gameOverPanel;
    public GameObject winningText;
	public GameObject[] subscenes;
	public GameObject nextButton;
	public GameObject previousButton;
	public Text sceneNum;
    public ActiveScene activeScene;
    private ActiveScene prevActive;

    public enum ActiveScene { Scene1 = 0, Scene2, Scene3, Scene4, Scene5 };    

    private int current = 0;

    private void OnValidate()
    {
        if (activeScene != prevActive)
        {
            // change active scene when dropdown is selected in Inspector
            for (int i = 0; i < subscenes.Length; i++)
            {
                subscenes[i].SetActive((int)activeScene == i);
            }
            prevActive = activeScene;
        }
    }

    void Awake()
	{
        sceneNum.text = "1/" + subscenes.Length;

        // make sure first scene is active
        subscenes[0].SetActive(true);
        for (int i = 1; i < subscenes.Length; i++)
        {
            subscenes[i].SetActive(false);
        }
    }

	public void Next()
	{
		current++;
		SwitchActive(current - 1, current);
		UpdateSceneNum();

        // Make sure to reset disabled color or "click" button
        ResetDisabledColor(current);

        if (current + 1 == subscenes.Length) nextButton.SetActive(false);
		else if(!previousButton.activeSelf) previousButton.SetActive(true);
	}

	public void Previous()
	{
		current--;
		SwitchActive(current + 1, current);
		UpdateSceneNum();

        // Make sure to reset disabled color or "click" button
        ResetDisabledColor(current);

        if (current == 0) previousButton.SetActive(false);
		else if(!nextButton.activeSelf) nextButton.SetActive(true);
	}

	void SwitchActive(int n1, int n2)
	{
		subscenes[n1].SetActive(!subscenes[n1].activeSelf);
		subscenes[n2].SetActive(!subscenes[n2].activeSelf);
	}

	void UpdateSceneNum()
	{
		sceneNum.text = (current + 1) + sceneNum.text.Substring(1);
	}

    private void ResetDisabledColor(int sceneNum)
    {
        // offset for array index vs. scene number
        sceneNum++;
        int buttonNum;

        // determine which button's disabled color to reset 
        switch (sceneNum)
        {
            case 1:
            case 5:
                return;
            case 2:
                buttonNum = 4;
                break;
            case 3:
                buttonNum = 1;
                break;
            case 4:
                buttonNum = 5;
                break;
            default:
                return;
        }

        // reference to button
        var buttonObject = GameObject.Find("Scene" + sceneNum.ToString() + "/GamePanel/Button" + buttonNum.ToString());
        if (buttonObject == null) return;

        var button = buttonObject.GetComponent<Button>();
        if (button == null) return;

        // create color block, change its disabled color to white, then set the button's color block to it
        var newColorBlock = button.colors;
        newColorBlock.disabledColor = new Color(1, 1, 1);
        button.colors = newColorBlock;
    }

    /// <summary>
    /// Only called on the last How To Play scene when Button9 is clicked
    /// </summary>
    public void LastSceneButtonClick()
    {
        // create array of buttons we want to change colors
        GameObject[] buttons = new GameObject[]
        {
            GameObject.Find("Scene5/GamePanel/Button5"),
            GameObject.Find("Scene5/GamePanel/Button6"),
            GameObject.Find("Scene5/GamePanel/Button8"),
            GameObject.Find("Scene5/GamePanel/Button9")
        };

        // blue/white colors for the image
        Color blue = new Color(0.0f, 214 / 255f, 214 / 255f);
        Color white = new Color(208 / 255f, 208 / 255f, 208 / 255f);

        // swap between blue/white for each button in the array
        foreach (var button in buttons)
        {            
            var imageColor = button.GetComponent<Image>().color;            
            if (imageColor == blue) imageColor = white;
            else imageColor = blue;
            button.GetComponent<Image>().color = imageColor;
        }

        // start the Game Over sequence
        screenFader.SetActive(true);
        gameOverPanel.SetActive(true);
        winningText.SetActive(true);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Exit()
    {
        SceneManager.LoadScene("StartScreen");
    }
    
}
