using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager instance;

    [Header("StartMenu")]
    [SerializeField] private GameObject startMenu;
    [Header("ListView")]
    [SerializeField] private GameObject listView;
    [Header("SearchView")]
    [SerializeField] private GameObject searchView;
    [Header("MapView")]
    [SerializeField] private GameObject mapView;
    [Header("GastromorphPage")]
    [SerializeField] private GameObject gastromorph;
    [Header("ListButton")]
    [SerializeField] private GameObject listButton;
    [Header("SearchButton")]
    [SerializeField] private GameObject searchButton;
    [Header("MapButton")]
    [SerializeField] private GameObject mapButton;
    [Header("SettingsButton")]
    [SerializeField] private GameObject settingsBtn;

    [HideInInspector] public bool mapBtn = false;
    [HideInInspector] public bool returnMap = false;
    [HideInInspector] public bool returnList = false;
    [HideInInspector] public bool returnSearch = false;


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
        gastromorph.SetActive(true);
        gastromorph.SetActive(false);
        listButton.SetActive(false);
        searchButton.SetActive(false);
        mapButton.SetActive(false);
        settingsBtn.SetActive(false);
    }
    public void startButtons()
    {
        listButton.SetActive(!listButton.activeSelf);
        searchButton.SetActive(!searchButton.activeSelf);
        mapButton.SetActive(!mapButton.activeSelf);
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
    public void startGastromorph(bool isFromSearch)
    {
        if (isFromSearch) returnSearch = true;
        else returnList = true;
        ActiveGastroPage();
    }

    private void ActiveGastroPage()
    {
        startMenu.SetActive(false);
        searchView.SetActive(false);
        mapView.SetActive(false);
        listView.SetActive(false);
        gastromorph.SetActive(true);
    }

    public void volver()
    {
        if (!CheckReturnBool())
        {
            startMenu.SetActive(true);
            settingsBtn.SetActive(false);
            searchView.SetActive(false);
            mapView.SetActive(false);
            listView.SetActive(false);
            gastromorph.SetActive(false);
            StopAllCoroutines();
            mapBtn = false;
        }
    }

    private bool CheckReturnBool() 
    {
        if (returnList) { startList(); returnList = false; return true; }
        if (returnSearch) { startSearch(); returnSearch = false; return true; }
        if (returnMap) { startMap(); returnMap = false; return true; }
        return false;
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

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_ANDROID
        AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
        activity.Call<bool>("moveTaskToBack", true);
        Debug.Log("Adiós");      
#else
        Application.Quit();
#endif

    }
}
