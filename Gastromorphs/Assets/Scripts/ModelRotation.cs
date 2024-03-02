using UnityEngine;
using UnityEngine.EventSystems;

public class ModelRotation : MonoBehaviour, IDragHandler
{
    public Transform[] models;
    public float velocidadRotation = 1f;
    //public ModelNames modelName;
    //public enum ModelNames
    //{
    //    Guindilava,
    //    Taco
    //}

    private void Awake()
    {
        foreach (var model in models)
        {
            model.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        foreach (Transform model in models)
        {
            if (model.gameObject.name == "Guindilava")
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
