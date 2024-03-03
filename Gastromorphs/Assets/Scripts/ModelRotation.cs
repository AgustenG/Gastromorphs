using UnityEngine;
using UnityEngine.EventSystems;

public class ModelRotation : MonoBehaviour, IDragHandler
{
    public Transform[] models;

    public float velocidadRotation = 1f;

    public static ModelRotation Instance;


    private void Awake()
    {
        Instance = this;
        foreach (var model in models)
        {
            model.gameObject.SetActive(false);
        }
    }

    public void ActivateModel(string name)
    {
        foreach (Transform model in models)
        {
            if (model.gameObject.name == name)
            {
                model.gameObject.SetActive(true);
            }
            else
            {
                model.gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    public void OnDrag(PointerEventData evenData)
    {
        float rotX = evenData.delta.x * velocidadRotation;
        foreach(Transform model in models)
        {
            if(model.gameObject.activeSelf)
            {
                model.Rotate(Vector3.up, -rotX, Space.World);
            }
        }
    }
}