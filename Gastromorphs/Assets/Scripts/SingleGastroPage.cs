using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SingleGastroPage : MonoBehaviour
{

    [SerializeField] GameObject content;
    [SerializeField] TextMeshProUGUI gastromorphName;
    [SerializeField] TextMeshProUGUI gastromorphDescription;
    [SerializeField] GameObject banner;
    [SerializeField] GameObject InstantiatePlace;

    private Sprite[] Sprites;


    public static SingleGastroPage Instance;

    private void Awake()
    {
        Instance = this;

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
