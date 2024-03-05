using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [Header("------------ Audio Source --------------")]
    public AudioClip backgroundMusic;
    private AudioSource audioSource;
    public List<AudioClip> clickBoton;

    [Header("------------ Toggle --------------")]
    public Toggle musicToggle;

    void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = backgroundMusic;
        audioSource.loop = true;

        if (musicToggle != null)
        {
            musicToggle.onValueChanged.AddListener(ToggleMusic);
        }
        
        Instance = this;
        
    }
    void Start()
    {
        ToggleMusic(musicToggle.isOn);
    }
    public void clickBotton()
    {
        audioSource.PlayOneShot(clickBoton[0]);
        audioSource.PlayOneShot(clickBoton[1]);
    }
    public void clickGeneral()
    {
        audioSource.PlayOneShot(clickBoton[0]);
        audioSource.PlayOneShot(clickBoton[5]);
    }
    public void volver()
    {
        audioSource.PlayOneShot(clickBoton[2]);
        audioSource.PlayOneShot(clickBoton[5]);
    }
    public void locationMap(bool isOn)
    {
        if (isOn)
        {
            audioSource.PlayOneShot(clickBoton[3]);
        }
    }
    public void clickToggle()
    {
        audioSource.PlayOneShot(clickBoton[6]);
    }
    public void ToggleMusic(bool isOn)
    {
        if (isOn)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
    }
}