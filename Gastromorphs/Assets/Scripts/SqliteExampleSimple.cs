using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System.Collections.Generic;
using System;
using System.Linq;

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

    private void GetBiomes(){



    }


    private void GetGastromorphs()
    {
        List<Biome> biomes = new List<Biome>();
        List<Element> elements = new List<Element>();
        List<Flavour> flavours = new List<Flavour>();

        dbCommandReadValue.CommandText = "SELECT * FROM \"main\".\"Biomes\"";
        using (IDataReader dataReader = dbCommandReadValue.ExecuteReader())
        {
            while (dataReader.Read())
            {
                int biomeID = dataReader.GetInt32(0);
                string name = (string)dataReader["name"];
                string desc = (string)dataReader["description"];
                string iconUri = (string)dataReader["biomeUri"];

                biomes.Add(new Biome(biomeID, name, desc, iconUri));
            }
        }
        dbCommandReadValue.Dispose();

        dbCommandReadValue = dbConnection.CreateCommand();
        dbCommandReadValue.CommandText = "SELECT * FROM \"main\".\"Elements\"";
        using (IDataReader dataReader = dbCommandReadValue.ExecuteReader())
        {
            while (dataReader.Read())
            {
                int elementID = dataReader.GetInt32(0);
                string name = (string)dataReader["name"];
                string desc = (string)dataReader["description"];
                string iconUri = (string)dataReader["elementUri"];

                elements.Add(new Element(elementID, name, desc, iconUri));
            }
        }
        dbCommandReadValue.Dispose();

        dbCommandReadValue = dbConnection.CreateCommand();
        dbCommandReadValue.CommandText = "SELECT * FROM \"main\".\"Flavours\"";
        using (IDataReader dataReader = dbCommandReadValue.ExecuteReader())
        {
            while (dataReader.Read())
            {
                int flavourID = dataReader.GetInt32(0);
                string name = (string)dataReader["name"];
                string desc = (string)dataReader["description"];
                string iconUri = (string)dataReader["flavourUri"];

                flavours.Add(new Flavour(flavourID, name, desc, iconUri));
            }
        }
        dbCommandReadValue.Dispose();

        dbCommandReadValue = dbConnection.CreateCommand();
        dbCommandReadValue.CommandText = "SELECT * FROM \"main\".\"Gastromorphs\"";
        using (IDataReader dataReader = dbCommandReadValue.ExecuteReader())
        {
            while (dataReader.Read())
            {
                List<int> tempBiomeList = new List<int>();
                List<int> tempElementList = new List<int>();
                List<int> tempFlavourList = new List<int>();

                List<Biome> biomeList = new List<Biome>();
                List<Element> elementList = new List<Element>();
                List<Flavour> flavourList = new List<Flavour>();

                int gastroId = dataReader.GetOrdinal("gastromorph_id");
                string name = (string)dataReader["name"];
                string desc = (string)dataReader["description"];

                string biomeIds = (string)dataReader["biome_ids"];
                tempBiomeList = biomeIds.Split(",").Select(int.Parse).ToList();
                foreach (int biomeId in tempBiomeList)
                {
                    foreach (Biome biome in biomes)
                    {
                        if (biome.ID == biomeId)
                        {
                            biomeList.Add(biome);
                        }
                    }
                }

                string elementIds = (string)dataReader["element_ids"];
                tempElementList = elementIds.Split(",").Select(int.Parse).ToList();
                foreach (int elementId in tempBiomeList)
                {
                    foreach (Element element in elements)
                    {
                        if (element.ID == elementId)
                        {
                            elementList.Add(element);
                        }
                    }
                }

                string flavourIds = (string)dataReader["flavour_ids"];
                tempFlavourList = flavourIds.Split(",").Select(int.Parse).ToList();
                foreach (int flavourId in tempFlavourList)
                {
                    foreach (Flavour flavour in flavours)
                    {
                        if (flavour.ID == flavourId)
                        {
                            flavourList.Add(flavour);
                        }
                    }
                }

                string iconUri = (string)dataReader["iconUri"];
                string animUri = (string)dataReader["modelUri"];

                Gastromorph newGastromorph = new Gastromorph(gastroId, name, desc, biomeList, elementList, flavourList);
                //GastromorphsManager.Instance.AllGastromorphs.Add(newGastromorph);

                Debug.Log(newGastromorph.Biomes[0].Name);
                Debug.Log(newGastromorph.Biomes[1].Name);
            }
        }
        dbCommandReadValue.Dispose();
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
