using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Data;
using Mono.Data.SqliteClient;
using System;
using System.Reflection;
using Assets._Scripts;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameController : MonoBehaviour {

	public Image[] buttons;					//array of buttons that the player will be tapping
	public Image[] panels;					//array of the panels on the solution board
	public Image[] stars;					//array of stars on the game over panel
	public Text[] solutionText;				//array of solution number text that goes on the goal configuration
	public Text movesText;					//reference to the number of moves text
	public Text gameOverMovesText;			//reference to the moves text on the game over panel
	public Text perfectText;				//reference to the perfect text on the game over panel
	public GameObject screenFader;			//reference to the screen fade image 
	public GameObject gameOverPanel;		//reference to the game over panel
	public GameObject menuButton;			//reference to the menu button

	public int[] startConfig = new int[9];	//public array to determine starting config
	public int[] solnConfig = new int[9];	//public array to determine solution config

    public Text levelText;                  //text for level number
    public Image levelPanel;                //image of level panel

	private int STAR_RANGE = 10;			//number of moves between each star level
	private int col;						//the button number the was pressed, corresponding to the column in the config matrix
	private int numMoves;					//current number of moves
	private int threeStar;					//number of moves under this number will give three stars
	private int twoStar;					//number of moves under this and over threeStar will give two stars
	private int starCount; 					//number of stars earned this level
	private int perfectScore; 				//number of moves for a perfect score

	//databse variables
	private string construct;
	private IDbConnection dbc;
	private IDbCommand dbcom;
	private IDataReader dbr;


	public bool test;      					//set to true in the editor if you wish to test a configuration (or anything else really)
											//test == true means the GameData variables will not be used

	//variables in the GameData script
	private string tableName;				//name of table to update and load from
	private int levelNum;					//level num we want to load
    [Range(2,11)]
	public int NUM_STATES;					//current number of states
	public bool random;						//is the game in random mode?
    public bool showSolution;               //do we want to show the solution on the smaller grid?

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


	//color used to set an image to visible
	private Color alphaOn = new Color(1f, 1f, 1f, 1f);

	//static instance of this class
	private static GameController instance = null;
	

	//declare current configuration and solution configuration arrays
	private int[] current = new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0};
	private int[] solution = new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0};

	//declare matrix that will determine which tiles change when a specific one is pressed
	//this matrix is the backbone of the entire game!!!
	private int[,] config = new int[,] {{1, 1, 0, 1, 0, 0, 0, 0, 0, 0},
										{1, 1, 1, 0, 1, 0, 0, 0, 0, 0},
										{0, 1, 1, 0, 0, 1, 0, 0, 0, 0},
										{1, 0, 0, 1, 1, 0, 1, 0, 0, 0},
										{1, 0, 1, 0, 1, 0, 1, 0, 1, 0},
										{0, 0, 1, 0, 1, 1, 0, 0, 1, 0},
										{0, 0, 0, 1, 0, 0, 1, 1, 0, 0},
										{0, 0, 0, 0, 1, 0, 1, 1, 1, 0},
										{0, 0, 0, 0, 0, 1, 0, 1, 1, 0}};


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

    /**********************
	 * Awake
	 **********************/
    void Awake ()
	{
		if(instance == null)
			//create an instance of GameController if it doesn't already exist
			instance = this;
		else if(instance != this)
			//if an instance already exists, destroy this one
			Destroy(gameObject);

        //disable solution text if it should be
        for (int i = 0; i < solutionText.Length; i++)
        {
            solutionText[i].enabled = showSolution;
        }


		//setup the level
		Setup();

	}



	/**********************
	 * Start
	 **********************/
	void Start()
	{

		//set up our star ranges
		threeStar = STAR_RANGE * NUM_STATES;
		twoStar = threeStar * 2;

		//solve the puzzle on start
		Solve ();

		perfectScore = counter;
	}

	void OnValidate()
	{
		//make sure the desired states are acceptable numbers
		for(int i = 0; i < startConfig.Length; i++)
		{
			startConfig[i] = Mathf.Clamp(startConfig[i], 0, NUM_STATES - 1);
			solnConfig[i] = Mathf.Clamp(solnConfig[i], 0, NUM_STATES - 1);
		}

	}


	/// <summary>
	/// Setup the game area
	/// </summary>
	void Setup()
	{


        if (!test)
		{
			tableName = GameData.tableName;
			levelNum = GameData.levelNum;
			NUM_STATES = GameData.numStates;
			random = GameData.random;	
		}

        // set color of level stuff
        if(levelPanel != null) levelPanel.color = stateColors[NUM_STATES - 1];
        if (levelText != null) levelText.color = stateColors[NUM_STATES - 1];

		if(!random)
		{
            if (levelText != null) levelText.text = "Level " + levelNum.ToString().PadLeft(3, '0');

            if (!test)
			{
                try
                {
                    // Get the correct levels based on which mode we're on
                    var t = Type.GetType("Assets._Scripts.LevelConfig_3_" + NUM_STATES.ToString());
                    var inst = Activator.CreateInstance(t);

                    int[,,] levelSetup = (int[,,])t.GetProperty("Setups", BindingFlags.Public | BindingFlags.Instance).GetValue(inst, null);

                    for (int i = 0; i < startConfig.Length; i++)
                    {
                        startConfig[i] = levelSetup[levelNum - 1, 0, i];
                        solnConfig[i] = levelSetup[levelNum - 1, 1, i];
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    // Level doesn't exist, just go back to mode select
                    SceneManager.LoadScene("ModeSelect");
                }
			}


			//set up the game based on the desired values 
			for(int i = 0; i < buttons.Length; i++)
			{
				buttons[i].color = stateColors[startConfig[i]];
				current[i] = startConfig[i];
				
				panels[i].color = stateColors[solnConfig[i]];
				solution[i] = solnConfig[i];
			}
		}
		else
		{
            //randomly set the starting configuration

            if (levelText != null) levelText.text = "Random";


            int flag;		//variable to store random numbers
			for(int i = 0; i < current.Length; i++)
			{
				//generate a random number
				flag = UnityEngine.Random.Range(0, NUM_STATES);
				
				
				//set this button's color and this element in the current array
				buttons[i].color = stateColors[flag];
				current[i] = flag;
				
			}
			
			//randomly set the goal configuration
			for(int i = 0; i < solution.Length; i++)
			{
				//generate a random number
				flag = UnityEngine.Random.Range(0, NUM_STATES);
				
				//set this panel's color and this element in the solution array
				panels[i].color = stateColors[flag];
				solution[i] = flag;
				
			}
		}
	}



	/// <summary>
	/// Updates the moves text with the number of moves
	/// </summary>
	/// <param name="x">The number of moves</param>
	void UpdateMovesText(int x)
	{
		movesText.text = "Moves: " + x;
	}

    /// <summary>
    /// Called when the Hint button is clicked; show 1 hint text
    /// </summary>
    public void ShowHint()
    {
        //loop through the solution text objects and enable a random one
        //if all are enabled, do nothing


        int flag;
        int iter = 0;
        bool allEnabled = true;

        do
        {
            //assume all are enabled until proven wrong
            allEnabled = true;

            //loop through text, if we find a disabled one, break
            for (int i = iter; i < solutionText.Length; i++)
            {
                if (!solutionText[i].enabled)
                {
                    //set iter as a starting point for next loop since we know every 
                    //element before this one is already enabled
                    iter = i;
                    allEnabled = false;
                    break;
                }
            }

            //generate a random int
            flag = UnityEngine.Random.Range(0, solutionText.Length);

            //if we find a disabled one, enable it, solve to update text properly, and break
            if (!solutionText[flag].enabled)
            {
                solutionText[flag].enabled = true;
                Solve();
                break;
            }            

        } while (!allEnabled);

    }


	/// <summary>
	/// Changes states of the appropriate buttons
	/// </summary>
	/// <param name="buttonNum">Button that was pressed</param>
	public void ChangeStates(int buttonNum)
	{
		//set the column number for the config array
		col = buttonNum - 1;

		//iterate through each row of the config array
		for(int i = 0; i <= config.GetUpperBound(0); i++)
		{
			//if we find a 1, that means this button should change states
			if(config[i, col] == 1)
			{

				//advance this button to the next state
				current[i] = (current[i] + 1) % NUM_STATES;
				buttons[i].color = stateColors[current[i]];
			}

		}

		//call the solve function to log the new winning solution
		Solve ();
		//update the moves text
		UpdateMovesText(++numMoves);


		//check if the user has won
		if(MatrixMath.CompareArray(current, solution))
		{
			//user has won, call the game over function
			Invoke("GameOver", 0f);
		}
	}


	/// <summary>
	/// Performs the Game Over sequence
	/// </summary>
	void GameOver()
	{
		screenFader.SetActive(true);				//activate the screen fader
		gameOverPanel.SetActive(true);				//activate the game over panel
		menuButton.SetActive(false);				//deactivate the menu button
		gameOverMovesText.text = movesText.text;	//set the game over moves text

		//award appropriate number of stars
		stars[0].color = alphaOn;
		starCount++;

		if(numMoves < twoStar)
		{
			stars[1].color = alphaOn;
			starCount++;
		}
		if(numMoves < threeStar)
		{
			stars[2].color = alphaOn;
			starCount++;
		}
		if(numMoves <= perfectScore)
		{
			perfectText.color = new Color(242/255f, 160/255f, 0f, 1f); 
			starCount++;
		}

		if(!random)
		{
            var db = new TileShiftDbAccess(DbName);

            db.OpenDB();

            // if this score is better than the current score, update it
            int currentScore = db.SelectLevelInfo(tableName, levelNum)[0];

            if (starCount > currentScore)
            {
                db.UpdateLevelRecord(tableName, levelNum, "stars", starCount);
            }

            // if there is a next level, make sure it is unlocked
            var nextLevelInfo = db.SelectLevelInfo(tableName, levelNum + 1);
            if (nextLevelInfo[0] >= 0 && nextLevelInfo[1] == 0)
            {
                db.UpdateLevelRecord(tableName, levelNum + 1, "unlocked", 1);
            }

            db.CloseDB();
		}
	}


	/// <summary>
	/// Solve this puzzle, and print the solution to the console
	/// </summary>
	void Solve()
	{
		//matrix to be sent to the RREF function
		int[,] rref = new int[config.GetUpperBound(0) + 1, config.GetUpperBound(1) + 1];

		//array to hold the required state changes
		int[] final = new int[config.GetUpperBound(0) + 1];

		//subtract the current configuration from the solution configuration, and store the result in final
		//this is necessary to solve the system of equations (linear algebra)
		final = MatrixMath.SubtractArrayModX(solution, current, NUM_STATES);

		//set rref equal to config, element by element
		for(int i = 0; i <= config.GetUpperBound(0); i++)
		{
			for(int j = 0; j <= config.GetUpperBound(1); j++)
			{
				//we want the last column to be the same as the final array
				//necessary to construct the augmented matrix 
				if(j == config.GetUpperBound(1)) rref[i, j] = final[i];
				else rref[i, j] = config[i, j];
			}
		}

		//used to output the answer
		string answer = "";

		//row reduce the matrix
		rref = MatrixMath.RRefModX(rref, NUM_STATES);

		//make sure the last element of the answer array is either 0 or NUM_STATES - 5 when NUM_STATES is a multiple of 5;
		//otherwise the configuration will be impossible to solve
		if(NUM_STATES % 5 == 0 && rref[rref.GetUpperBound(0), rref.GetUpperBound(1)] != 0 && rref[rref.GetUpperBound(0), rref.GetUpperBound(1)] != NUM_STATES - 5)
		{
			int index = solution.Length - 1;
			solution[index] = (solution[index] + 1) % NUM_STATES;
			panels[index].color = stateColors[solution[index]];
			Solve();
			return;
		}

		if(NUM_STATES % 5 == 0 && NUM_STATES > 5 && rref[rref.GetUpperBound(0), rref.GetUpperBound(1)] == NUM_STATES - 5)
		{
			for(int i = 0; i < rref.GetUpperBound(0); i++)
			{
				int temp = rref[i, rref.GetUpperBound(1)] - rref[i, rref.GetUpperBound(1) - 1];
				while(temp < 0) temp += NUM_STATES;

				counter += temp;
				answer += " " + temp;
			}
			counter += 1;
			answer += " " + 1;

		}
		else
		{
			for(int i = 0; i <= rref.GetUpperBound(0); i++)
			{
				answer += " " + rref[i, rref.GetUpperBound(1)];
				counter += rref[i, rref.GetUpperBound(1)];
			}
		}

        // only update this if these labels are enabled
        for (int i = 0; i < solutionText.Length; i++)
        {
            if (solutionText[i].enabled)
            {
                solutionText[i].text = rref[i, rref.GetUpperBound(1)].ToString();
            }
        }
        
		Debug.Log(answer + "\n");

	}

	//count number of moves remaining for perfect score
	int counter;


	/// <summary>
	/// Loads the next level.
	/// Gets called from the "NextLevel" button on the game over panel
	/// </summary>
	public void NextLevel()
	{
		if(!random) GameData.levelNum++;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void NextLevelRandom()
	{
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

	/// <summary>
	/// Quit the game
	/// </summary>
	public void Quit()
	{
		string toLoad = random ? "ModeSelect" : "LevelSelect";
        SceneManager.LoadScene(toLoad);
	}

}