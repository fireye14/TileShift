using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets._Scripts
{
    public class TileShiftDbAccess : dbAccess
    {
        public TileShiftDbAccess(string dbName) : base(dbName)
        {
        }

        /// <summary>
        /// Attempt to create a level table of the given name. Does nothing if table already exists.
        /// columns: level INT, stars INT, unlocked BIT
        /// </summary>
        /// <param name="tableName">Name of table to create</param>
        public void CreateModeLevelTable(string tableName)
        {
            if (tableName == string.Empty) return;

            Dictionary<string, string> columns = new Dictionary<string, string>()
            {
                { "level", "INT PRIMARY KEY" },
                { "stars", "INT" },
                { "unlocked", "INT" }
            };

            try
            {
                CreateTable(tableName, columns);
            }
            catch (DbAccessException e)
            {
                DbAccessErrorHandler(e);
            }

        }

        /// <summary>
        /// Get info about number of stars earned and unlocked stats of a level in a given mode
        /// </summary>
        /// <param name="tableName">Table Name</param>
        /// <param name="levelNum">Level to select</param>
        /// <returns>array with structure { stars, unlocked }, -1s if record not found, -2s if error in read function</returns>
        public int[] SelectLevelInfo(string tableName, int levelNum)
        {

            //InsertInto(tableName, new string[] { "1", "0", "0" });

            var selectList = new string[] { "stars", "unlocked" };
            var where = new string[,] { { "level", "=", levelNum.ToString() } };

            try
            {
                // Sets the Reader property to returned results
                SelectWhere(tableName, selectList, where);

                // return number of stars for this level if a record exists, otherwise -1
                return Reader.Read() ? new int[] { (int)Reader["stars"], (int)Reader["unlocked"] } : new int[] { -1, -1 };
            }
            catch (DbAccessException e)
            {
                DbAccessErrorHandler(e);
                return new int[] { -2, -2 };
            }
        }

        /// <summary>
        /// Insert single record into level table; stars = 0, unlocked = false unless level = 1
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="levelNum"></param>
        public void InsertLevelRecord(string tableName, int levelNum)
        {
            try
            {
                InsertInto(tableName, new string[] { levelNum.ToString(), "0", levelNum == 1 ? "1" : "0" });
            }
            catch (DbAccessException e)
            {
                DbAccessErrorHandler(e);
            }
        }

        /// <summary>
        /// Updates a single level record with given field and value
        /// </summary>
        /// <param name="tableName">table name</param>
        /// <param name="levelNum">level to update</param>
        /// <param name="field">field to update</param>
        /// <param name="value">value to set the field to</param>
        public void UpdateLevelRecord(string tableName, int levelNum, string field, int value)
        {
            // this will be either star or unlocked value
            var colVals = new string[,] { { field, value.ToString() } };
            var where = new string[,] { { "level", "=", levelNum.ToString() } };

            try
            {
                UpdateTable(tableName, colVals, where);
            }
            catch (DbAccessException e)
            {
                DbAccessErrorHandler(e);
            }
        }

        /// <summary>
        /// Logs the DbAccessException to the console
        /// </summary>
        /// <param name="dbe"></param>
        public void DbAccessErrorHandler(DbAccessException dbe)
        {
            Debug.LogError(dbe.Message + Environment.NewLine + dbe.StackTrace);
        }
    }
}
