using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridElement : MonoBehaviour
{
    public GameObject prefab;

    

    public void SetElements(List<Element> elements)
    {

        foreach (Element element in elements)
        {
            GameObject go = Instantiate(prefab, this.transform);
            go.SetActive(true);

            go.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>($"{element.Name}");
          
        }

    }
}
