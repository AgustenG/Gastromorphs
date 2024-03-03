using System.Collections;
using UnityEngine;

public class MapController : MonoBehaviour 
{
    public float seconds;
    public GameObject Icon;

    private void Awake()
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
        yield return new WaitForSeconds(seconds);
        Icon.gameObject.SetActive(false);
        CanvasManager.instance.startSearch();
        Debug.Log(gameObject.transform.parent.gameObject.name);
        CanvasManager.instance.mapBtn = false;
    }
}
