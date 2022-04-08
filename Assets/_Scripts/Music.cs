using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Data;
using Mono.Data.SqliteClient;

public class Music : MonoBehaviour {

	public static Music music = null;

    public Text musicButtonText;

    private string construct;
    private IDbConnection dbc;
    private IDbCommand dbcom;
    private IDataReader dbr;
    private string tableName;           //settings table name; pulled from GameData class
    private bool onOff;                 //on/off music setting stored in the database
    private AudioSource source;         //audio source component of the object this script is attached to

    void Awake()
	{
        if (music != null && music != this)
        {
            music.musicButtonText = this.musicButtonText;

            //read music setting value
            tableName = GameData.settingsTable;
            construct = "URI=file:" + Application.persistentDataPath + "/Master.db";
            dbc = new SqliteConnection(construct);
            dbc.Open();

            dbcom = dbc.CreateCommand();

            try
            {
                dbcom.CommandText = "CREATE TABLE '" + tableName + "' (Setting TEXT, OnOff INT, Value INT)";
                dbcom.ExecuteNonQuery();
            }
            catch (SqliteSyntaxException)
            { /*table already exists*/ }

            //get the on/off setting from the table
            dbcom.CommandText = "SELECT OnOff FROM '" + tableName + "' WHERE Setting = 'music'";
            dbr = dbcom.ExecuteReader();

            if (dbr.Read())
            {
                // The music setting was found: set the music status accordingly
                onOff = (int)dbr["OnOff"] != 0;
            }
            else
            {
                // The music setting was not found: create it and initialize to On
                dbcom.CommandText = "INSERT INTO '" + tableName + "' (Setting, OnOff, Value) VALUES ('music', 1, 0)";
                dbcom.ExecuteNonQuery();
                onOff = true;
            }

            //set music button text
            music.musicButtonText.text = "Music: " + (onOff ? "On" : "Off");

            Destroy(gameObject);
        }


		else if(music == null)
		{
			music = this;
			DontDestroyOnLoad(gameObject);

            //read music setting value
            tableName = GameData.settingsTable;
            construct = "URI=file:" + Application.persistentDataPath + "/Master.db";
            dbc = new SqliteConnection(construct);
            dbc.Open();

            dbcom = dbc.CreateCommand();

            try
            {
                dbcom.CommandText = "CREATE TABLE '" + tableName + "' (Setting TEXT, OnOff INT, Value INT)";
                dbcom.ExecuteNonQuery();
            }
            catch (SqliteSyntaxException)
            { /*table already exists*/ }

            //get the on/off setting from the table
            dbcom.CommandText = "SELECT OnOff FROM '" + tableName + "' WHERE Setting = 'music'";
            dbr = dbcom.ExecuteReader();

            if (dbr.Read())
            {
                // The music setting was found: set the music status accordingly
                onOff = (int)dbr["OnOff"] != 0;
            }
            else
            {
                // The music setting was not found: create it and initialize to On
                dbcom.CommandText = "INSERT INTO '" + tableName + "' (Setting, OnOff, Value) VALUES ('music', 1, 0)";
                dbcom.ExecuteNonQuery();
                onOff = true;
            }

            //get referene to audio source and set its enabled to the table value
            source = GetComponent<AudioSource>();
            source.enabled = onOff;
            musicButtonText.text = "Music: " + (source.enabled ? "On" : "Off");

        }			
	}

}
