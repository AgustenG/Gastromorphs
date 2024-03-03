using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SingleGastroPage : MonoBehaviour
{

    [SerializeField] GameObject biomeContent;
    [SerializeField] GameObject flavourContent;
    [SerializeField] GameObject typeContent;

    [SerializeField] TextMeshProUGUI gastromorphName;
    [SerializeField] TextMeshProUGUI gastromorphDescription;
    [SerializeField] GameObject banner;

    [SerializeField] GameObject attributePrefab;


    private Sprite[] Sprites;


    public static SingleGastroPage Instance;

    private void Awake()
    {
        Instance = this;

    }

    public void SetGastromorphAttributes(Gastromorph gastromorph)
    {
        for (int i = 0; i < biomeContent.transform.childCount; i++)
        {
            GameObject child = biomeContent.transform.GetChild(i).gameObject;
            Destroy(child);
        }
        for (int i = 0; i < flavourContent.transform.childCount; i++)
        {
            GameObject child = flavourContent.transform.GetChild(i).gameObject;
            Destroy(child);
        }
        for (int i = 0; i < typeContent.transform.childCount; i++)
        {
            GameObject child = typeContent.transform.GetChild(i).gameObject;
            Destroy(child);
        }


        foreach (Biome biome in gastromorph.Biomes)
        {
           GameObject go =  Instantiate(attributePrefab, biomeContent.transform);

            go.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>($"Biomes/{biome.Name}");
            go.SetActive(true);
        }
        foreach (Type type in gastromorph.Type)
        {
            GameObject go = Instantiate(attributePrefab, typeContent.transform);

            go.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>($"Types/{type.Name}");
            go.SetActive(true);
        }
        foreach (Flavour flavour in gastromorph.Flavours)
        {
            GameObject go = Instantiate(attributePrefab, flavourContent.transform);

            go.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>($"Flavours/{flavour.Name}");
            go.SetActive(true);
        }
    }


    public void OpenGastromorph(Gastromorph gastromorph)
    {
        if (gastromorph == null) return;
        gastromorphName.text = gastromorph.Name;
        gastromorphDescription.text = gastromorph.Description;


        Sprites = Resources.LoadAll<Sprite>("Biomes/Elements");
        Sprite sprite = GetSpriteByName(gastromorph.Biomes[0].Name);

        banner.GetComponent<Image>().sprite = sprite;

    }
    public Sprite GetSpriteByName(string name)
    {
        for (int i = 0; i < Sprites.Length; i++)
        {
            if (Sprites[i].name == name)
                return Sprites[i];
        }
        return null;
    }
}
