using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;




public class GastromorphsManager : MonoBehaviour
{
    [SerializeField]
    private List<Gastromorph> allGastromorphs = new();
    public List<Gastromorph> AllGastromorphs
    {
        get { return allGastromorphs; }
        private set { allGastromorphs = value; }
    }

    [SerializeField]
    private List<Biome> allBiomes = new();
    public List<Biome> AllBiomes
    {
        get { return allBiomes; }
        private set { allBiomes = value; }
    }

    [SerializeField]
    private List<Element> allElements = new();
    public List<Element> AllElements
    {
        get { return allElements; }
        private set { allElements = value; }
    }

    [SerializeField]
    private List<Flavour> allFlavours = new();
    public List<Flavour> AllFlavours
    {
        get { return allFlavours; }
        private set { allFlavours = value; }
    }

    public int GastromorphCount
    {
        get { return AllGastromorphs.Count; }
    }
    public int BiomesCount
    {
        get { return AllBiomes.Count; }
    }
    public int ElementsCount
    {
        get { return AllElements.Count; }
    }
    public int FlavoursCount
    {
        get { return AllFlavours.Count; }
    }

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

    public void InstantiateGastromorph()
    {
        foreach (Gastromorph gastromorph in AllGastromorphs)
        {
            GameObject prefab = (GameObject)Resources.Load($"{gastromorph.Name}");

            Instantiate(prefab);
            prefab.transform.position = Vector3.zero;
        }

    }

}
