using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Driver;

public class dataBaseMD : MonoBehaviour
{
    private MongoClient client;
    private IMongoDatabase database;

    // Cambia estas variables según tu configuración de MongoDB
    private string connectionString = "mongodb://localhost:27017";
    private string databaseName = "tuBaseDeDatos";

    void Start()
    {
        ConnectToMongoDB();
    }

    void ConnectToMongoDB()
    {
        client = new MongoClient(connectionString);

        if (client != null)
        {
            Debug.Log("Conexión exitosa a MongoDB.");

            // Selecciona la base de datos
            database = client.GetDatabase(databaseName);

            // Puedes hacer más operaciones aquí, como trabajar con colecciones, insertar datos, etc.
        }
        else
        {
            Debug.LogError("No se pudo conectar a MongoDB.");
        }
    }
}
