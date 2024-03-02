using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using Unity.VisualScripting;
using System;

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
    private List<Type> allElements = new();
    public List<Type> AllElements
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

    public List<Gastromorph> filteredList = new();

    private List<string> selectedTypes = new();
    private List<string> selectedBiomes = new();
    private List<string> selectedFlavours = new();

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
    private void Start()
    {
        filteredList = new List<Gastromorph>(allGastromorphs);
    }
    public void ToggleTypes(string type)
    {
        if (selectedTypes.Contains(type))
        {
            // Toggle off the element filter
            selectedTypes.Remove(type);
        }
        else
        {
            // Toggle on the element filter
            selectedTypes.Add(type);
        }

        ApplyFilters();

    }
    public void ToggleFlavours(string flavours)
    {
        if (selectedFlavours.Contains(flavours))
        {
            // Toggle off the element filter
            selectedFlavours.Remove(flavours);
        }
        else
        {
            // Toggle on the element filter
            selectedFlavours.Add(flavours);
        }
        ApplyFilters();

    }
    public void ToggleBiomes(string biomes)
    {

        if (selectedBiomes.Contains(biomes))
        {
            // Toggle off the element filter
            selectedBiomes.Remove(biomes);
        }
        else
        {
            // Toggle on the element filter
            selectedBiomes.Add(biomes);
        }
        ApplyFilters();
    }

    public Gastromorph GetGastromorphFromId(string id)
    {
        foreach (Gastromorph item in allGastromorphs)
        {
            if (item.Gastromorph_id == Convert.ToInt32(id))
                return item;
        }
        return null;
    }
    private void ApplyFilters()
    {
        filteredList.Clear();
        foreach (Gastromorph gastromorph in AllGastromorphs)
        {
            foreach (Biome biome in gastromorph.Biomes)
            {
                if (!selectedBiomes.Contains(biome.Name)) continue;
            }
            foreach (Flavour flavour in gastromorph.Flavours)
            {
                if (!selectedFlavours.Contains(flavour.Name)) continue;

            }
            foreach (Type type in gastromorph.Type)
            {
                if (!selectedTypes.Contains(type.Name)) continue;
            }

            filteredList.Add(gastromorph);
        }
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
