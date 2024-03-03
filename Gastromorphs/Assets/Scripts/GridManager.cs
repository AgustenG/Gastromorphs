using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using static UnityEngine.Rendering.DebugUI;

public class GridManager : MonoBehaviour
{
    public GameObject toggablePrefab;
    public GameObject gastromorphPrefab;
    public GameObject gastromorphPrefabSimple;
    private List<GameObject> filterTogglesObjs = new List<GameObject>();
    private string textInput;
    [SerializeField] private TMP_InputField inputField;
    public List<Toggle> filterToggles = new List<Toggle>();
    private List<GameObject> instantiatedGastromorphs = new List<GameObject>();

    [Tooltip("g0,t1,b2,f3")]
    [SerializeField] GameObject[] parentContent;
    [SerializeField] GastromorphsManager gManager;

    private enum Parents
    {
        Gastromorph = 0,
        Type = 1,
        Biome = 2,
        Flavour = 3,
        GastroIcon = 4
    }

    private static GridManager instance;
    public static GridManager Instance
    {
        get { return instance; }
    }

    private void Start()
    {
        textInput = "";
    }

    private void Awake()
    {
        instance = this;
    }
    public void TogglesListeners()
    {
        inputField.onValueChanged.AddListener(GetInputText);

        foreach (GameObject item in parentContent)
        {
            if (item.name == parentContent[0].name || item.name == parentContent[4].name) continue;

            for (int i = 0; i < item.transform.childCount; i++)
                filterTogglesObjs.Add(item.transform.GetChild(i).gameObject);
        }

        int count = 0;
        foreach (GameObject obj in filterTogglesObjs)
        {
            filterToggles.Add(obj.GetComponent<Toggle>());          
            filterToggles[count].onValueChanged.AddListener(OnToggleValueChanged);
            count++;
        }
    }

    private void OnToggleValueChanged(bool isOn)
    {
        FilterGastromorphs();
    }

    void GetInputText(string filterText)
    {
        textInput = filterText;
        FilterGastromorphs();
    }

    public void SetIconsGastromorphs(List<Gastromorph> gastromorphsList)
    {
        foreach (Gastromorph gastromorph in gastromorphsList)
        {
            GameObject go = Instantiate(gastromorphPrefabSimple, parentContent[(int)Parents.GastroIcon].transform);
            go.SetActive(true);

            go.GetComponentsInChildren<Image>(true)[1].sprite = Resources.Load<Sprite>($"Gastromorphs/{gastromorph.Name}");

            go.GetComponentInChildren<TextMeshProUGUI>().text = gastromorph.Gastromorph_id.ToString();

            instantiatedGastromorphs.Add(go);
        }
    }

    public void SetGastromorphs(List<Gastromorph> gastromorphsList)
    {
        foreach (Gastromorph gastromorph in gastromorphsList)
        {
            GameObject go = Instantiate(gastromorphPrefab, parentContent[(int)Parents.Gastromorph].transform);
            go.SetActive(true);

            go.GetComponentsInChildren<Image>(true)[1].sprite = Resources.Load<Sprite>($"Gastromorphs/{gastromorph.Name}");

            go.GetComponentsInChildren<TextMeshProUGUI>()[1].text = gastromorph.Gastromorph_id.ToString();
            go.GetComponentsInChildren<TextMeshProUGUI>()[0].text = gastromorph.Name;

            instantiatedGastromorphs.Add(go);
        }
    }

    public void FilterGastromorphs()
    {
        foreach (GameObject go in instantiatedGastromorphs)
        {
            Destroy(go);
        }

        List<Gastromorph> gastromorphs = gManager.AllGastromorphs;
        List<Gastromorph> filteredGastromorphs = new List<Gastromorph>();

        string textFilters = textInput;

        List<string> attFilters = new List<string>();

        foreach (Toggle toggle in filterToggles)
        {
            if (toggle.isOn)
            {
                attFilters.Add(toggle.gameObject.GetComponentInChildren<TextMeshProUGUI>().text);
            }
        }

        int filtersCount = attFilters.Count;
        int hasFiltersCount = 0;
        bool textMatch = false;

        foreach (Gastromorph gastromorph in gastromorphs)
        {
            hasFiltersCount = 0;
            if (textFilters != string.Empty)
            {
                if (gastromorph.Name.ToLower().Contains(textFilters.ToLower()) || gastromorph.Gastromorph_id.ToString().Contains(textFilters))
                {
                    if (attFilters.Count > 0)
                    {
                        foreach (string attFilter in attFilters)
                        {
                            foreach (Biome biome in gastromorph.Biomes)
                            {
                                if (biome.Name == attFilter)
                                {
                                    hasFiltersCount++;
                                }
                            }

                            foreach (Flavour flavour in gastromorph.Flavours)
                            {
                                if (flavour.Name == attFilter)
                                {
                                    hasFiltersCount++;
                                }
                            }

                            foreach (Type type in gastromorph.Type)
                            {
                                if (type.Name == attFilter)
                                {
                                    hasFiltersCount++;
                                }
                            }
                        }
                    }
                    textMatch = true;
                }
                else textMatch = false;
            }
            else if (filtersCount > 0)
            {           
                foreach (string attFilter in attFilters)
                {
                    //Debug.Log(attFilter);
                    foreach (Biome biome in gastromorph.Biomes)
                    {
                        if (biome.Name == attFilter)
                        {
                            hasFiltersCount++;
                        }
                    }

                    foreach (Flavour flavour in gastromorph.Flavours)
                    {
                        if (flavour.Name == attFilter)
                        {
                            hasFiltersCount++;
                        }
                    }

                    foreach (Type type in gastromorph.Type)
                    {
                        if (type.Name == attFilter)
                        {
                            hasFiltersCount++;
                        }
                    }
                }
                textMatch = true;
            }
            else 
                textMatch = true; 
      
            if (textMatch)
            {
                if (filtersCount > 0)
                {
                    if (hasFiltersCount == filtersCount)
                    {
                        filteredGastromorphs.Add(gastromorph);
                    }
                }
                else { filteredGastromorphs.Add(gastromorph); }
            }
        }

        SetGastromorphs(filteredGastromorphs);
    }

    public void SetBiomes(List<Biome> biomes)
    {
        foreach (Biome biome in biomes)
        {
            GameObject go = Instantiate(toggablePrefab, parentContent[(int)Parents.Biome].transform);
            go.SetActive(true);

            go.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>($"Biomes/{biome.Name}");
            go.GetComponentInChildren<TextMeshProUGUI>().text = biome.Name;
        }
    }
    public void SetTypes(List<Type> types)
    {

        foreach (Type type in types)
        {
            GameObject go = Instantiate(toggablePrefab, parentContent[(int)Parents.Type].transform);
            go.SetActive(true);

            go.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>($"Types/{type.Name}");
            go.GetComponentInChildren<TextMeshProUGUI>().text = type.Name;
        }

    }

    public void SetFlavours(List<Flavour> flavours)
    {
        foreach (Flavour flavour in flavours)
        {
            GameObject go = Instantiate(toggablePrefab, parentContent[(int)Parents.Flavour].transform);
            go.SetActive(true);

            go.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>($"Flavours/{flavour.Name}");
            go.GetComponentInChildren<TextMeshProUGUI>().text = flavour.Name;
        }
    }
}
