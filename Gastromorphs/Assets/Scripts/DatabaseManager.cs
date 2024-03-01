using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using System.IO;

public class DatabaseManager : MonoBehaviour
{
    IDbConnection dbConnection;
    IDbCommand dbCommandInsertValue;
    IDbCommand dbCommandReadValue;

    [SerializeField] private GridManager gridManager;

    [SerializeField] private GastromorphsManager manager;

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

        SetGastromorphsFromDB();
    }

    private void OnDisable()
    {
        dbConnection.Close();
        Debug.Log("Database disconnected");
    }



    private void SetBiomesFromDB()
    {
        dbCommandReadValue.CommandText = "SELECT * FROM \"main\".\"Biomes\"";
        using (IDataReader dataReader = dbCommandReadValue.ExecuteReader())
        {
            while (dataReader.Read())
            {
                int biomeID = dataReader.GetInt32(0);
                string name = (string)dataReader["name"];
                string desc = (string)dataReader["description"];
                string iconUri = "";//(string)dataReader["uri"];

                manager.AllBiomes.Add(new Biome(biomeID, name, desc, iconUri));
            }
        }
        gridManager.SetBiomes(manager.AllBiomes);

        dbCommandReadValue.Dispose();
    }

    private void SetElementsFromDB()
    {
        dbCommandReadValue = dbConnection.CreateCommand();
        dbCommandReadValue.CommandText = "SELECT * FROM \"main\".\"Types\"";
        using (IDataReader dataReader = dbCommandReadValue.ExecuteReader())
        {
            while (dataReader.Read())
            {
                int elementID = dataReader.GetInt32(0);
                string name = (string)dataReader["name"];
                string desc = (string)dataReader["description"];
                string iconUri = "";// (string)dataReader["uri"];

                manager.AllElements.Add(new Type(elementID, name, desc, iconUri));
            }
        }
        gridManager.SetTypes(manager.AllElements);
        dbCommandReadValue.Dispose();

    }

    private void SetFlavoursFromDB()
    {
        dbCommandReadValue = dbConnection.CreateCommand();
        dbCommandReadValue.CommandText = "SELECT * FROM \"main\".\"Flavours\"";
        using (IDataReader dataReader = dbCommandReadValue.ExecuteReader())
        {
            while (dataReader.Read())
            {
                int flavourID = dataReader.GetInt32(0);
                string name = (string)dataReader["name"];
                string desc = (string)dataReader["description"];
                string iconUri = "";//(string)dataReader["uri"];

                manager.AllFlavours.Add(new Flavour(flavourID, name, desc, iconUri));
            }
        }
        gridManager.SetFlavours(manager.AllFlavours);
        dbCommandReadValue.Dispose();
    }

    private void SetGastromorphsFromDB()
    {
        SetBiomesFromDB();
        SetElementsFromDB();
        SetFlavoursFromDB();


        dbCommandReadValue = dbConnection.CreateCommand();
        dbCommandReadValue.CommandText = SelectAllGastromorphs();
        using (IDataReader dataReader = dbCommandReadValue.ExecuteReader())
        {
            while (dataReader.Read())
            {
                //Get data from DB
                int gastroId = dataReader.GetInt32(dataReader.GetOrdinal("gastromorph_id"));
                string name = (string)dataReader["name"];
                string desc = (string)dataReader["description"];
                string biomeIds = (string)dataReader["biome_ids"];
                string elementIds = (string)dataReader["type_ids"];
                string flavourIds = (string)dataReader["flavour_ids"];
                string iconUri = (string)dataReader["iconUri"];
                string animUri = (string)dataReader["modelUri"];

                //Temp lists that will have 1 Gastromorph
                List<Biome> tempBiomes = new List<Biome>();
                List<Type> tempElements = new List<Type>();
                List<Flavour> tempFlavours = new List<Flavour>();

                // Check the biomes that the Gastromorph has in the DB and adds it to it list.
                foreach (int biomeId in biomeIds.Split(",").Select(int.Parse).ToList())
                {

                    foreach (Biome biome in manager.AllBiomes)
                    {
                        if (biome.ID == biomeId)
                            tempBiomes.Add(biome);
                    }
                }

                // Check the elements that the Gastromorph has in the DB and adds it to it list.
                foreach (int elementId in elementIds.Split(",").Select(int.Parse).ToList())
                {
                    foreach (Type element in manager.AllElements)
                    {
                        if (element.ID == elementId)
                        {
                            tempElements.Add(element);
                        }
                    }
                }

                //  Check the flavours that the Gastromorph has in the DB and adds it to it list.
                foreach (int flavourId in flavourIds.Split(",").Select(int.Parse).ToList())
                {
                    foreach (Flavour flavour in manager.AllFlavours)
                    {
                        if (flavour.ID == flavourId)
                        {
                            tempFlavours.Add(flavour);
                        }
                    }
                }
                manager.AllGastromorphs.Add(new(gastroId, name, desc, tempBiomes, tempElements, tempFlavours));
            }
        }
        gridManager.SetGastromorphs(manager.AllGastromorphs);
        dbCommandReadValue.Dispose();


        Destroy(this);
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
    private string InsertGastromorph(string name, string desc, string biome_ids, string element_ids, string flavour_ids, string iconuri, string modelUri)
    {
        return $"INSERT INTO \"main\".\"Gastromorphs\"\r\n(\"name\", \"description\", \"biome_ids\", \"element_ids\", \"flavour_ids\", \"iconUri\", \"modelUri\")\r\nVALUES ('{name}','{desc}','{biome_ids}','{element_ids}','{flavour_ids}','{iconuri}','{modelUri}')";
    }

    private IDbConnection CreateAndOpenDatabase()
    {
        string p = "Mydatabase.db";
        string dbUri = $"URI=file:{Application.streamingAssetsPath}/Mydatabase.db";
        string android = "URI=file:" + Application.persistentDataPath + "/" + p;



        string path = "jar:file://" + Application.dataPath + "!/assets/Mydatabase.db";

        WWW wwwfile = new WWW(path);
        while(wwwfile.isDone) { }
        var filepath = string.Format("{0}/{1}", Application.persistentDataPath, "Mydatabase.db");
        File.WriteAllBytes(filepath, wwwfile.bytes);
        

        IDbConnection dbConnection = new SqliteConnection(filepath);
        dbConnection.Open();
        return dbConnection;
    }
}
