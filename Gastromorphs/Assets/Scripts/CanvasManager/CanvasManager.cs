using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager instance;
    [Header("StartMenu")]
    [SerializeField] public GameObject startMenu;
    [Header("ListView")]
    [SerializeField] public GameObject listView;
    [Header("IconsView")]
    [SerializeField] public GameObject iconsView;
    [Header("MapView")]
    [SerializeField] public GameObject mapView;
    [Header("GastromorphPage")]
    [SerializeField] public GameObject gastromorph;
    [Header("ListButton")]
    [SerializeField] public GameObject listButton;
    [Header("IconsButton")]
    [SerializeField] public GameObject iconsButton;
    [Header("MapButton")]
    [SerializeField] public GameObject mapButton;


    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    private void Start()
    {
        startMenu.SetActive(true);
        listView.SetActive(false);
        iconsView.SetActive(false);
        mapView.SetActive(false);
        gastromorph.SetActive(false);
        Debug.Log("heyy");
        listButton.SetActive(false);
        iconsButton.SetActive(false);
        mapButton.SetActive(false);
        Time.timeScale = 1f;
    }
    public void startButtons()
    {
        Debug.Log("me abri");
        listButton.SetActive(true);
        iconsButton.SetActive(true);
        mapButton.SetActive(true);
        Time.timeScale = 1f;
    }

    public void startIcons()
    {
        startMenu.SetActive(false);
        iconsView.SetActive(true);
        Time.timeScale = 0f;
    }
    public void startMap()
    {
        startMenu.SetActive(false);
        iconsView.SetActive(false);
        mapView.SetActive(true);
        Time.timeScale = 0f;
    }
    public void startList()
    {
        startMenu.SetActive(false);
        iconsView.SetActive(false);
        mapView.SetActive(false);
        listView.SetActive(true);
        Time.timeScale = 0f;
    }
    public void startGastromorph()
    {
        startMenu.SetActive(false);
        iconsView.SetActive(false);
        mapView.SetActive(false);
        listView.SetActive(false);
        gastromorph.SetActive(true);
        Time.timeScale = 0f;
    }
    public void volver()
    {
        iconsView.SetActive(false);
        mapView.SetActive(false);
        listView.SetActive(false);
        gastromorph.SetActive(false);
        startMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    public void activeModel()
    {
        iconsView.SetActive(false);
        mapView.SetActive(false);
        listView.SetActive(false);
        gastromorph.SetActive(false);
        startMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void activateCanvas()
    {
        startGastromorph();
    }

    public void ExitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

    }
}