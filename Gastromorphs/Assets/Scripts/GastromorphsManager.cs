using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GastromorphsManager : MonoBehaviour
{
    public List<Gastromorph> AllGastromorphs;


    private static GastromorphsManager instance;

    public static GastromorphsManager Instance
    {
        get { return instance; }
    }

    private void Awake() 
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else 
            instance = this;
    }
}
