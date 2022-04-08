using UnityEngine;
using System.Collections;
using System.Data;
using Mono.Data.SqliteClient;
using System;

public class GameData : MonoBehaviour {

	public static GameData data;		//static instance of this class
	
	public static string tableName;		//name of the table to update and load from
	public static int numStates;		//number of states
	public static int levelNum;			//level number to load
	public static bool random;			//is the game in random mode?
    public static string settingsTable = "GameSettings"; //settings table name
	
	void Awake()
	{
		if(data == null)
		{
			//if data hasn't been set yet, set it to this instance, and don't destroy it
			DontDestroyOnLoad(gameObject);
			data = this;
		}
		else if(data != this)
		{
			//if data does exist, but isn't this instance, destroy this instance
			Destroy (gameObject);
		}
	}

    internal static void UpdateSettings(string setting, bool onOff = false, int value = 0)
    {
        string construct;
        IDbConnection dbc;
        IDbCommand dbcom;
        IDataReader dbr;

        construct = "URI=file:" + Application.persistentDataPath + "/Master.db";
        dbc = new SqliteConnection(construct);
        dbc.Open();
        dbcom = dbc.CreateCommand();

        dbcom.CommandText = "SELECT OnOff FROM '" + settingsTable + "' WHERE Setting = '" + setting + "'";
        dbr = dbcom.ExecuteReader();

        if (dbr.Read())
        {
            // setting record was found in table, update it
            dbcom.CommandText = "UPDATE '" + settingsTable + "' SET OnOff = " +
                  (onOff ? 1 : 0) + ", Value = " + value + " WHERE Setting = '" + setting + "'";

            dbcom.ExecuteNonQuery();
        }
        else
        {
            //either table does not exist or setting does not exist
            //do nothing as this is just the update method
        }
    }
}
