using UnityEngine;

public class Hola : MonoBehaviour
{
    void Start()
    {
        Gastromorph gastro = new Gastromorph(1, "hola");

        Debug.Log(gastro.IconUri);
    }
}