using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;

public class SqliteExampleSimple : MonoBehaviour
{
    IDbConnection dbConnection;
    IDbCommand dbCommandInsertValue;
    IDbCommand dbCommandReadValue;
    public enum Tables
    {
        Biome,
        Element,
        Flavour
    }

    private void Awake()
    {
        dbConnection = CreateAndOpenDatabase();
        dbCommandInsertValue = dbConnection.CreateCommand();
        dbCommandReadValue = dbConnection.CreateCommand();

    }

    private void OnDisable()
    {
        dbConnection.Close();
        Debug.Log("Database disconnected");
    }
    public void Click()
    {
        GetGastromorphs();

    }

    private void GetGastromorphs()
    {
        dbCommandReadValue.CommandText = "SELECT * FROM \"main\".\"Gastromorphs\"";
        IDataReader dataReader = dbCommandReadValue.ExecuteReader();
        
        while (dataReader.Read()) 
        {
            Gastromorph gastromorph = new Gastromorph(dataReader.GetInt32(0), dataReader.GetString(1));
            Debug.Log(gastromorph.Name + ": " + gastromorph.Gastromorph_id);
        }
        dbCommandReadValue.Parameters.Clear();  
        dataReader.Close();
    }

    private void DataBaseAction(string action)
    {
        try
        {
            dbCommandInsertValue.CommandText = action;
            dbCommandInsertValue.ExecuteNonQuery();
        }
        catch (System.Exception)
        {
            Debug.Log("no");
            throw;
        }

    }
    private string SelectAllGastromorphs()
    {
        return "SELECT * FROM \"main\".\"Gastromorphs\"";
    }
    private string DeleteTableData(string databasename)
    {
        return "DELETE FROM \"main\".\"Gastromorphs\"";
    }
    private string InsertAttributeIntoTable(Tables tab, string name, string desc, string iconuri)
    {
        string table = tab.ToString();
        return $"INSERT INTO \"main\".\"{table}\"\r\n(\"name\", \"description\", \"IconUri\")\r\nVALUES ('{name}','{desc}','{iconuri}')";
    }
    private string InsertGastromorph(string name, string desc, int biome_id, int element_id, int flavour_id, string iconuri, string modelUri)
    {
        return $"INSERT INTO \"main\".\"Gastromorphs\"\r\n(\"name\", \"description\", \"biome_id\", \"element_id\", \"flavour_id\", \"iconUri\", \"modelUri\")\r\nVALUES ('{name}','{desc}','{biome_id}',{element_id},{flavour_id},'{iconuri}','{modelUri}')";
    }

    private IDbConnection CreateAndOpenDatabase()
    {
        string dbUri = "URI=file:Assets/StreamingAssets/MyDatabase.db";
        IDbConnection dbConnection = new SqliteConnection(dbUri);
        dbConnection.Open();
        return dbConnection;
    }
}
