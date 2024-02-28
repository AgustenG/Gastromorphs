using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapController : MonoBehaviour/*, IPointerClickHandler*/
{
    public GameObject Icon;

    public void ActiveIcon()
    {
        Icon.SetActive(true);
        Icon.GetComponent<Animator>().Play("AnimLocation");
    }

    //private void Start()
    //{
    //    Icon.SetActive(true);
    //    Icon.GetComponent<Animator>().enabled = true;
    //}
    //public void OnPointerClick(PointerEventData eventData)
    //{
    //    Debug.Log("d");
    //    transform.GetComponentInChildren<RawImage>().gameObject.SetActive(true);
    //}

    //public void disableIcon()
    //{
    //    transform.GetComponentInChildren<RawImage>().enabled = false;
    //}
}
