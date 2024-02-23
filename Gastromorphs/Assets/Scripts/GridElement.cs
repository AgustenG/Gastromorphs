using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridElement : MonoBehaviour
{
    public GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 7; i++)
        {
            GameObject go = Instantiate(prefab, this.transform);
            go.SetActive(true);
        }
    }
}
