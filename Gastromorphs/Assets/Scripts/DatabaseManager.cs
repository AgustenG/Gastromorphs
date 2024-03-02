using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine.Networking;
using System.Collections;

public class DatabaseManager : MonoBehaviour
{
    IDbConnection dbConnection;
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


#if UNITY_EDITOR        // For Editor
        dbConnection = GetDBFromStreamingAssets();
        Debug.Log("Opening From editor");
#elif UNITY_ANDROID     // For Android  
        StartCoroutine(SetDatabase());
        dbConnection = GetDatabaseFromPersistentDataPath();
        Debug.Log("Opening From Android");
#else                   // For Windows ? 
        dbConnection = GetDBFromStreamingAssets();
        Debug.Log("Opening From windows");
#endif

        dbConnection.Open();
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
                string iconUri = "";//(string)dataReader["iconUri"];
                string animUri = "";// (string)dataReader["modelUri"];

                //Temp lists that will have 1 Gastromorph
                List<Biome> tempBiomes = new();
                List<Type> tempElements = new();
                List<Flavour> tempFlavours = new();

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

    private string SelectAllGastromorphs()
    {
        return "SELECT * FROM \"main\".\"Gastromorphs\"";
    }
    /// <summary>
    /// For unity for android
    /// </summary>
    /// <returns></returns>
    private IDbConnection GetDatabaseFromPersistentDataPath()
    {
        string path = "URI=file:" + Application.persistentDataPath + "/Mydatabase.sqlite";
        IDbConnection dbConnection = new SqliteConnection(path);
        return dbConnection;
    }

    /// <summary>
    /// For unity for windows
    /// </summary>
    /// <returns></returns>
    private IDbConnection GetDBFromStreamingAssets()
    {
        string dbUri = "URI=file:Assets/StreamingAssets/MyDatabase.sqlite";
        IDbConnection dbConnection = new SqliteConnection(dbUri);
        return dbConnection;
    }
    private IEnumerator SetDatabase()
    {
        string path;
        // For Android
        path = "jar:file://" + Application.dataPath + "!/assets/Mydatabase.sqlite";

        if (!File.Exists("URI=file:" + Application.persistentDataPath + "/Mydatabase.sqlite"))
        {
            Debug.Log("CREATE DATABASE AGAIN");
            UnityWebRequest unityWebRequest = UnityWebRequest.Get(path);

            unityWebRequest.downloadHandler = new DownloadHandlerBuffer();

            yield return unityWebRequest.SendWebRequest();

            if (unityWebRequest.result == UnityWebRequest.Result.Success)
            {
                // Retrieve results as binary data.
                byte[] data = unityWebRequest.downloadHandler.data;

                // Writes the DB in the persistent memory.
                File.WriteAllBytes(Path.Combine(Application.persistentDataPath, "Mydatabase.sqlite"), data);
            }
            else
            {
                Debug.LogError("Failed to download database. Error: " + unityWebRequest.error);
            }
        }
    }



}
