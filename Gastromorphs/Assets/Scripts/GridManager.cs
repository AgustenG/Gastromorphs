using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    public GameObject toggablePrefab;
    public GameObject gastromorphPrefab;

    [Tooltip("g0,t1,b2,f3")]
    [SerializeField] GameObject[] parentContent;

    private enum Parents
    {
        Gastromorph = 0,
        Type = 1,
        Biome = 2,
        Flavour = 3
    }

    private static GridManager instance;
    public static GridManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;

    }

    public void SetGastromorphs(List<Gastromorph> gastromorphs)
    {
        foreach (Gastromorph gastromorph in gastromorphs)
        {
            GameObject go = Instantiate(gastromorphPrefab, parentContent[(int)Parents.Gastromorph].transform);
            go.SetActive(true);

            go.GetComponentsInChildren<Image>(true)[1].sprite = Resources.Load<Sprite>($"Gastromorphs/{gastromorph.Name}");
           
            go.GetComponentsInChildren<TextMeshProUGUI>()[0].text = gastromorph.Gastromorph_id.ToString();
            go.GetComponentsInChildren<TextMeshProUGUI>()[1].text = gastromorph.Name;
        }
    }

    public void SetBiomes(List<Biome> biomes)
    {
        foreach (Biome biome in biomes)
        {
            GameObject go = Instantiate(toggablePrefab, parentContent[(int)Parents.Biome].transform);
            go.SetActive(true);

           go.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>($"Biomes/{biome.Name}");

        }
    }
    public void SetTypes(List<Type> types)
    {

        foreach (Type type in types)
        {
            GameObject go = Instantiate(toggablePrefab, parentContent[(int)Parents.Type].transform);
            go.SetActive(true);

            go.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>($"Types/{type.Name}");

        }

    }

    public void SetFlavours(List<Flavour> flavours)
    {
        foreach (Flavour flavour in flavours)
        {
            GameObject go = Instantiate(toggablePrefab, parentContent[(int)Parents.Flavour].transform);
            go.SetActive(true);

            go.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>($"Flavours/{flavour.Name}");

        }
    }
}
