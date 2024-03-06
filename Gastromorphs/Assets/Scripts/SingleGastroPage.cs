using System.Collections;
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
    [SerializeField] TextMeshProUGUI gastromorphID;
    [SerializeField] GameObject banner;

    [SerializeField] GameObject attributePrefab;
    [SerializeField] Image spinImage;


    public static SingleGastroPage Instance;

    private void Awake()
    {
        Instance = this;

    }

    public void SetGastromorphAttributes(Gastromorph gastromorph)
    {
        DeletePastGastromorphs();

        foreach (Biome biome in gastromorph.Biomes)
        {
            GameObject go = Instantiate(attributePrefab, biomeContent.transform);

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

    private void DeletePastGastromorphs()
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

    }

    public void OpenGastromorph(Gastromorph gastromorph)
    {
        if (gastromorph == null) return;
        gastromorphName.text = gastromorph.Name;
        gastromorphDescription.text = gastromorph.Description;
        gastromorphID.text = gastromorph.Gastromorph_id.ToString();

        Sprite sprite = Resources.Load<Sprite>($"BiomeBanners/{gastromorph.Biomes[0].Name}");

        banner.GetComponent<Image>().sprite = sprite;
      
       // StartCoroutine(SpinImage());

    }
    private IEnumerator SpinImage()
    {
       
        float currentTime = 0.0f;

        do
        {
            spinImage.fillAmount = Mathf.Lerp(0, 1, currentTime / 1f);
            currentTime += Time.deltaTime;
            yield return null;
        } while (currentTime <= 1f);
        spinImage.fillAmount = 1f;
    }
}
