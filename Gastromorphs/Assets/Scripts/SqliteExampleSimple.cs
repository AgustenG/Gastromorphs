using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System.Linq;

public class SqliteExampleSimple : MonoBehaviour
{
    public Texture2D TexRef;
    IDbConnection dbConnection;
    IDbCommand dbCommandInsertValue;

    string[] insertableTables = { "Biome", "Element", "Flavour" };
    private void Awake()
    {
        dbConnection = CreateAndOpenDatabase();
        dbCommandInsertValue = dbConnection.CreateCommand();
    }

    private void OnDisable()
    {
        dbConnection.Close();
        Debug.Log("Database disconnection");
    }
    public void Click()
    {
        DataBaseAction(InsertGastromorph("Guindilava", "Guindilla de lava", 1, 1, 1, "test.png", "test.fbx"));
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
    private string DeleteTableData(string databasename)
    {
        return "DELETE FROM \"main\".\"Gastromorphs\"";
    }
    private string InsertAttributeIntoTable(string table, string name, string desc, string iconuri)
    {
        if (insertableTables.Contains<string>(table)) {

            return $"INSERT INTO \"main\".\"{table}\"\r\n(\"name\", \"description\", \"IconUri\")\r\nVALUES ('{name}','{desc}','{iconuri}')";
        }
        return null;
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
