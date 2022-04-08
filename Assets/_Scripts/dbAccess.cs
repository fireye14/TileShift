using UnityEngine;
using System;
using System.Collections;
using System.Data;
using System.Text;
using Mono.Data.SqliteClient;
using Assets;
using System.Collections.Generic;
using System.Linq;

public class dbAccess
{
    public dbAccess(string dbName)
    {
        if (dbName.Substring(dbName.Length - 3) != ".db") dbName = dbName + ".db";
        DbName = dbName;
    }

    /// <summary>
    /// Connection string to Master database
    /// </summary>
    private string ConnectionString
    {
        get
        {
            return "URI=file:" + Application.persistentDataPath + "/" + DbName;
        }
    }

    /// <summary>
    /// True when connection has been initialized and opened, false otherwise
    /// </summary>
    internal bool ConnOpen
    {
        get
        {
            return (Con != null && Con.State == ConnectionState.Open);
        }
    }

    private string DbName { get; set; }

    protected IDbConnection Con { get; set; }
    protected IDbCommand Cmd { get; set; }
    protected IDataReader Reader { get; set; }
	
    /// <summary>
    /// Initialize and open the connection to the database
    /// </summary>
	public void OpenDB()
	{
        if (Con != null) CloseDB();
		Con = new SqliteConnection(ConnectionString);
		Con.Open();
	}
	
    /// <summary>
    /// Cleans everything up and closes the connection to the database
    /// </summary>
	public void CloseDB()
    {
        if (Reader != null)
        {
            Reader.Close(); 
            Reader = null;
        }
        if (Cmd != null)
        {
            Cmd.Dispose();
            Cmd = null;
        }
        if (Con != null)
        {
            Con.Close();
            Con = null;
        }
	}

    /// <summary>
    /// Create a table with given name, columns, and column types
    /// </summary>    
    /// <param name="name">Table name</param>
    /// <param name="cols">Dictionary of column names and types</param>    
    /// <exception cref="DbAccessException">Thrown when connection to DB has not been opened yet</exception>
    public void CreateTable(string name, Dictionary<string, string> cols)
    {
        if (!ConnOpen) throw new DbAccessException("Connection to database is not open");

        var colsList = cols.ToList();
        var query = "CREATE TABLE IF NOT EXISTS '" + name + "' (";

        for (var i = 0; i < cols.Count; i++)
        {
            if (i > 0) query += ", ";
            query += colsList[i].Key.Trim() + " " + colsList[i].Value.Trim();
        }
        query += ")";

        try
        {            
            Cmd = Con.CreateCommand();
            Cmd.CommandText = query;
            Cmd.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            throw new DbAccessException("Create Table failed: " + e.Message, e);
        }
    }

    /// <summary>
    /// Selects a list of columns from the given table with the given conditions
    /// </summary>
    /// <param name="tableName">Table to select from</param>
    /// <param name="selectList">Column names to select</param>
    /// <param name="where">WHERE conditions. Ex. {"ID", "=", "69"}</param>
    /// <returns>2D string array with returned rows/columns</returns>
    /// <exception cref="DbAccessException">Thrown when connection to DB has not been opened yet</exception>
    public void SelectWhere(string tableName, string[] selectList, string[,] where)
    {
        if (!ConnOpen) throw new DbAccessException("Connection to database is not open");

        var query = "SELECT ";

        // build columns to select into query
        for (var i = 0; i < selectList.Length; i++)
        {
            if (i > 0) query += ", ";
            query += selectList[i].Trim();
        }

        query += " FROM '" + tableName + "' WHERE 1=1 ";

        // build where conditions into query
        for (var i = 0; i <= where.GetUpperBound(0); i++)
        {
            query += " AND " + where[i, 0] + where[i, 1] + where[i, 2];
        }
        
        try
        {
            // attempt to execute the query
            Cmd = Con.CreateCommand();
            Cmd.CommandText = query;
            Reader = Cmd.ExecuteReader();
        }
        catch(Exception e)
        {
            throw new DbAccessException("Read failed: " + e.Message, e);
        } 
    }

    /// <summary>
    /// Inserts a record into the given table with the given values
    /// </summary>
    /// <param name="tableName">Table to insert a record to</param>
    /// <param name="values">List of values</param>
    /// <exception cref="DbAccessException">Thrown when the connection is not open or the insert fails</exception>
    public void InsertInto(string tableName, string[] values)
    {
        if (!ConnOpen) throw new DbAccessException("Connection to database is not open");

        var query = "INSERT INTO '" + tableName + "' VALUES (";
        for (int i = 0; i < values.Length; i++)
        {
            if (i > 0) query += ", ";
            query += values[i];
        }
        query += ")";

        try
        {
            Cmd = Con.CreateCommand();
            Cmd.CommandText = query;
            Cmd.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            throw new DbAccessException("Insert failed: " + e.Message, e);
        }
    }

    /// <summary>
    /// Update a given table's rows to specified values where given conditions are met
    /// </summary>
    /// <param name="tableName">Table Name</param>
    /// <param name="colVals"> SET updates. Ex. { "stars", "2" }</param>
    /// <param name="where">WHERE conditions. Ex. {"ID", "=", "69"}</param>
    /// <exception cref="DbAccessException">Thrown if connection to database not open or the update failed</exception>
    public void UpdateTable(string tableName, string[,] colVals, string[,] where)
    {
        if (!ConnOpen) throw new DbAccessException("Connection to database is not open");

        var query = "UPDATE '" + tableName + "' SET ";

        // build SET 
        for (int i = 0; i <= colVals.GetUpperBound(0); i++)
        {
            if (i > 0) query += ", ";
            query += colVals[i, 0] + " = " + colVals[i, 1];
        }

        query += " WHERE 1=1 ";

        // build where conditions into query
        for (var i = 0; i <= where.GetUpperBound(0); i++)
        {
            query += " AND " + where[i, 0] + where[i, 1] + where[i, 2];
        }

        try
        {
            Cmd = Con.CreateCommand();
            Cmd.CommandText = query;
            Cmd.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            throw new DbAccessException("Update failed: " + e.Message, e);
        }
    }		
}