using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ModelRotation : MonoBehaviour, IDragHandler, IPointerClickHandler
{
    public Transform[] models;

    public float velocidadRotation = 1f;

    public static ModelRotation Instance;

    private bool isAnimating = false;

    private float scalingSpeed = 0.5f;



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
        foreach (Transform model in models)
        {
            if (model.gameObject.activeSelf)
            {
                model.Rotate(Vector3.up, -rotX, Space.World);
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isAnimating) return;
        StartCoroutine(ScaleOverTime(scalingSpeed));    
    }

    IEnumerator ScaleOverTime(float time)
    {
        isAnimating = true;
        foreach (Transform model in models)
        {
            if (model.gameObject.activeSelf)
            {
                Vector3 originalScale = model.transform.localScale;
                Vector3 destinationScale = new(1.2f, 1.2f, 1.2f);

                float currentTime = 0.0f;

                do
                {
                    model.transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / time);
                    currentTime += Time.deltaTime;
                    yield return null;
                } while (currentTime <= time);
                StartCoroutine(DescaleOverTime(scalingSpeed,originalScale));
            }
        }     
    }

    IEnumerator DescaleOverTime(float time, Vector3 startingScale)
    {      
        foreach (Transform model in models)
        {
            if (model.gameObject.activeSelf)
            {
                Vector3 originalScale = model.transform.localScale;
                Vector3 destinationScale = startingScale;

                float currentTime = 0.0f;

                do
                {
                    model.transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / time);
                    currentTime += Time.deltaTime;
                    yield return null;
                } while (currentTime <= time);
                isAnimating = false;
            }
        }
      
    }
}