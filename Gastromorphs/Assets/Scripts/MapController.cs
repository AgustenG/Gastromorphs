using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapController : MonoBehaviour 
{
    public float seconds;
    public GameObject Icon;

    private void OnEnable()
    {
        Icon.SetActive(false);
    }

    public void StartFilter()
    {
        if(CanvasManager.instance.mapBtn == false)
        {
            CanvasManager.instance.mapBtn = true;
            StartCoroutine(Filter());
        }
    }

    private IEnumerator Filter()
    {
        Icon.gameObject.SetActive(true);
        AudioManager.Instance.locationMap(true);
        yield return new WaitForSeconds(seconds);
        AudioManager.Instance.locationMap(false);
        Icon.gameObject.SetActive(false);
        CanvasManager.instance.startSearch();
        foreach (Toggle toggle in GridManager.Instance.filterToggles)
        {
            if (toggle.gameObject.GetComponentInChildren<TextMeshProUGUI>().text == gameObject.transform.parent.gameObject.name)
            {
                toggle.isOn = true;
            }
            else toggle.isOn = false;
        }
        CanvasManager.instance.mapBtn = false;
        CanvasManager.instance.returnMap = true;
    }
}
