using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager instance;
    [Header("StartMenu")]
    [SerializeField] public GameObject startMenu;
    [Header("ListView")]
    [SerializeField] public GameObject listView;
    [Header("SearchView")]
    [SerializeField] public GameObject searchView;
    [Header("MapView")]
    [SerializeField] public GameObject mapView;
    [Header("GastromorphPage")]
    [SerializeField] public GameObject gastromorph;
    [Header("ListButton")]
    [SerializeField] public GameObject listButton;
    [Header("SearchButton")]
    [SerializeField] public GameObject searchButton;
    [Header("MapButton")]
    [SerializeField] public GameObject mapButton;

    [Header("SettingsButton")]
    [SerializeField] public GameObject settingsBtn;

    [HideInInspector] public bool mapBtn = false;


    void Awake()
    {
        if (instance != null && instance != this) Destroy(this);
        else instance = this;
    }
    private void Start()
    {
        startMenu.SetActive(true);
        listView.SetActive(false);
        searchView.SetActive(false);
        mapView.SetActive(false);
        gastromorph.SetActive(false);
        listButton.SetActive(false);
        searchButton.SetActive(false);
        mapButton.SetActive(false);
        settingsBtn.SetActive(false);
        Time.timeScale = 1f;
    }
    public void startButtons()
    {
        listButton.SetActive(!listButton.activeSelf);
        searchButton.SetActive(!searchButton.activeSelf);
        mapButton.SetActive(!mapButton.activeSelf);
        Time.timeScale = 1f;
    }

    public void startSearch()
    {
        startMenu.SetActive(false);
        searchView.SetActive(true);
    }
    public void startMap()
    {
        startMenu.SetActive(false);
        searchView.SetActive(false);
        mapView.SetActive(true);
    }
    public void startList()
    {
        startMenu.SetActive(false);
        searchView.SetActive(false);
        mapView.SetActive(false);
        listView.SetActive(true);
    }
    public void startGastromorph()
    {
        startMenu.SetActive(false);
        searchView.SetActive(false);
        mapView.SetActive(false);
        listView.SetActive(false);
        gastromorph.SetActive(true);
    }
    public void volver()
    {
        settingsBtn.SetActive(false);
        searchView.SetActive(false);
        mapView.SetActive(false);
        listView.SetActive(false);
        gastromorph.SetActive(false);
        startMenu.SetActive(true);
    }

    public void activateCanvas()
    {
        startGastromorph();
    }

    public void startSettings()
    {
        settingsBtn.SetActive(true);
        searchView.SetActive(false);
        mapView.SetActive(false);
        listView.SetActive(false);
        gastromorph.SetActive(false);
        startMenu.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}