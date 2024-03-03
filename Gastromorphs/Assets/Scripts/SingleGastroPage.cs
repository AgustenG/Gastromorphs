using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SingleGastroPage : MonoBehaviour
{

    [SerializeField] GameObject content;
    [SerializeField] TextMeshProUGUI gastromorphName;
    [SerializeField] TextMeshProUGUI gastromorphDescription;
    [SerializeField] GameObject banner;

    private Sprite[] Sprites;


    public static SingleGastroPage Instance;

    private void Awake()
    {
        Instance = this;

    }

    //public void SetGastromorphAttributes(Gastromorph gastromorph)
    //{
    //    foreach (Biome biome in gastromorph.Biomes)
    //    {
    //        GameObject go = Instantiate(gastromorphPrefab, parentContent[(int)Parents.Gastromorph].transform);
    //        go.SetActive(true);

    //        go.GetComponentsInChildren<Image>(true)[1].sprite = Resources.Load<Sprite>($"Gastromorphs/{gastromorph.Name}");

    //        go.GetComponentsInChildren<TextMeshProUGUI>()[0].text = gastromorph.Gastromorph_id.ToString();
    //        go.GetComponentsInChildren<TextMeshProUGUI>()[1].text = gastromorph.Name;
    //    }

    //}
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
