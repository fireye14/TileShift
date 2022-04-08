using Assets;
using Assets._Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevels : MonoBehaviour
{
    #region Fields

    public GameObject LevelButton;
    public Transform ButtonParent;
    public Text ModeText;
    public Text StarsText;

    private readonly Color[] stateColors = new Color[] {
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
		new Color(30/255f, 30/255f, 30/255f)};          //state ten = black	

    private static Color perfectColor = new Color(242 / 255f, 160 / 255f, 0f, 1f);
    private static Color starColor = new Color(1f, 1f, 1f, 1f);

    #endregion

    #region Properties

    /// <summary>
    /// Name of table for levels in this mode
    /// </summary>
    private string TableName
    {
        get
        {
            return GameData.tableName;
        }
    }

    /// <summary>
    /// Number of different color tiles in this mode
    /// </summary>
    private int NumStates
    {
        get
        {
            return GameData.numStates;
        }
    }

    /// <summary>
    /// "Master.db"
    /// </summary>
    private string DbName
    {
        get
        {
            return "Master.db";
        }
    }

    /// <summary>
    /// Instance of UpdateStars class for current LevelButtonClone
    /// </summary>
    private UpdateStars UpdateStars
    {
        get
        {
            return LevelButtonClone.GetComponent<UpdateStars>();
        }
    }

    /// <summary>
    /// RectTransform of current LevelButtonClone
    /// </summary>
    private RectTransform Rect
    {
        get
        {
            return LevelButtonClone.GetComponent<RectTransform>(); 
        }
    }

    /// <summary>
    /// Image of current LevelButtonClone
    /// </summary>
    private Image Image
    {
        get
        {
            return LevelButtonClone.GetComponent<Image>();
        }
    }

    /// <summary>
    /// Button component of current LevelButtonClone
    /// </summary>
    private Button Button
    {
        get
        {
            return LevelButtonClone.GetComponent<Button>();
        }
    }

    /// <summary>
    /// Number of stars player has earned for current level in loop; must select level info first
    /// </summary>
    private int NumStars
    {
        get
        {
            return LevelInfo == null ? -1 : LevelInfo[0];
        }
    }

    /// <summary>
    /// Whether or not the current level in loop is unlocked; must select level info first
    /// </summary>
    private bool Unlocked
    {
        get
        {
            return LevelInfo == null ? false : LevelInfo[1] == 1;
        }
    }

    private GameObject LevelButtonClone { get; set; }
    private int[] LevelInfo { get; set; }
    private int TotalStarCount { get; set; }
    #endregion
    
    void Awake()
    {
        // set invisible until all processing is done
        ButtonParent.gameObject.SetActive(false);

        // get appropriate level setups for selected number of colors
        var t = Type.GetType("Assets._Scripts.LevelConfig_3_" + NumStates.ToString());
        var inst = Activator.CreateInstance(t);

        int[,,] levelSetup = (int[,,])t.GetProperty("Setups", BindingFlags.Public | BindingFlags.Instance).GetValue(inst, null);

        var db = new TileShiftDbAccess(DbName);

        // Open connection and make sure a table exists for this mode
        db.OpenDB();        
        db.CreateModeLevelTable(TableName);

        // Create a button for each level in this mode, and make sure a record exists for it in the table
        for (var i = 0; i <= levelSetup.GetUpperBound(0); i++)
        {
            // create instance of button Prefab
            LevelButtonClone = Instantiate(LevelButton, ButtonParent, false);

            // Get number of stars the player has earned for this level
            LevelInfo = db.SelectLevelInfo(TableName, (i + 1));
            if (NumStars == -1)
            {
                // no record exists for this level; insert one                
                db.InsertLevelRecord(TableName, i + 1);

                // If this is the first level, make sure unlocked is true
                if (i + 1 == 1) LevelInfo[1] = 1;
            }
            else if (NumStars >= 0)
            {
                // Level has been completed and has stars for it; update the stars sprites on the button
                for (var j = 0; j < NumStars; j++)
                {
                    // NumStars == 4 means perfect score achieved
                    if (j != 3) UpdateStars.stars[j].color = starColor;
                    else UpdateStars.perfectText.color = perfectColor;                    
                }

                // Max of 3 stars earned per level
                TotalStarCount += Mathf.Clamp(NumStars, 0, 3);
            }

            // disable button if level not unlocked
            Button.interactable = Unlocked;

            // set OnClick event if unlocked
            if (Unlocked)
            {
                // set Button's onclick event to start appropriate level
                // make local copy of i so that correct param gets sent
                var param = i + 1;
                Button.onClick.AddListener(delegate { StartLevel(param); });
            }

            // set correct position
            Rect.localPosition = new Vector3(0, i * -300);
            Rect.offsetMin = new Vector2(10, Rect.offsetMin.y);
            Rect.offsetMax = new Vector2(-10, Rect.offsetMax.y);

            // set level text
            UpdateStars.levelText.text = "Level " + (i + 1).ToString().PadLeft(3, '0');

            // set color of button
            Image.color = stateColors[NumStates - 1];

        }

        // Close DB connection
        db.CloseDB();

        // set back to visible
        ButtonParent.gameObject.SetActive(true);

        // set Mode and Stars text
        ModeText.text = NumStates.ToString() + " Colors";
        StarsText.text = TotalStarCount.ToString().PadLeft(3, '0');

    }


    public void StartLevel(int levelNum)
    {
        GameData.levelNum = levelNum;
        SceneManager.LoadScene("Game_3");
    }

    public void GoBack()
    {
        SceneManager.LoadScene("ModeSelect");
    }
}
