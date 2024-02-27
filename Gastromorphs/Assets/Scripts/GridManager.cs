using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    public GameObject prefab;

    public void SetGastromorphs(List<Gastromorph> gastromorphs)
    {
        foreach (Gastromorph gastromorph in gastromorphs)
        {
            GameObject go = Instantiate(prefab, this.transform);
            go.SetActive(true);

            go.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>($"Gastromorphs/{gastromorph.Name}");

            Debug.Log(gastromorph.Gastromorph_id);
            go.GetComponentsInChildren<TextMeshProUGUI>()[0].text = gastromorph.Gastromorph_id.ToString();
            go.GetComponentsInChildren<TextMeshProUGUI>()[1].text = gastromorph.Name;
        }
    }

    public void SetBiomes(List<Biome> biomes)
    {
        foreach (Biome biome in biomes)
        {
            GameObject go = Instantiate(prefab, this.transform);
            go.SetActive(true);

            go.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>($"Biomes/{biome.Name}");

        }
    }


    public void SetElements(List<Element> elements)
    {

        foreach (Element element in elements)
        {
            GameObject go = Instantiate(prefab, this.transform);
            go.SetActive(true);

            go.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>($"Elements/{element.Name}");
          
        }

    }

    public void SetFlavours(List<Flavour> flavours)
    {
        foreach (Flavour flavour in flavours)
        {
            GameObject go = Instantiate(prefab, this.transform);
            go.SetActive(true);

            go.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>($"Flavours/{flavour.Name}");

        }
    }
}
