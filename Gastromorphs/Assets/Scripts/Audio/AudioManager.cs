using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //[DoNotSerialize] public static AudioManager instance;
    [Header("------------Audio Source --------------")]
    //[SerializeField] AudioSource musicSource;
    //[SerializeField] AudioSource sfxSource;
    private AudioSource audioSource;
    public List<AudioClip> clickBoton;


    //[Header("------------Audio Clips -------------")]

    //public AudioClip clickButton;

    //public AudioClip menuTheme;
    //public AudioClip gameTheme;
    //public AudioClip gameOverTheme;
    //public AudioClip attack;
    //public AudioClip jump;
    //public AudioClip collectCoin;
    //public AudioClip killEnemy;


    //void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //        DontDestroyOnLoad(gameObject);

    //    }
    //    else Destroy(gameObject);
    //    musicSource.volume = 0.35f;

    //}
    void Awake()
    {
        //musicSource.volume = 0.35f;
        ////musicSource.clip = menuTheme;
        //musicSource.Play();
        audioSource = GetComponent<AudioSource>();
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
    public void locationMap()
    {
        audioSource.PlayOneShot(clickBoton[3]);
    }
    public void scroll()
    {
        audioSource.PlayOneShot(clickBoton[4]);
    }
    //public void StartGame()
    //{
    //    musicSource.Stop();
    //    musicSource.clip = gameTheme;
    //    musicSource.Play();
    //}
    //public void StopMusic()
    //{
    //    musicSource.Stop();
    //}
    //public void StartMenuTheme()
    //{
    //    if (sfxSource.isPlaying)
    //    {
    //        sfxSource.Stop();
    //    }
    //    if (musicSource.isPlaying)
    //    {
    //        musicSource.Stop();
    //    }
    //    musicSource.clip = menuTheme;
    //    musicSource.Play();
    //}

    //public void StartGameOverTheme()
    //{
    //    if (sfxSource.isPlaying)
    //    {
    //        sfxSource.Stop();
    //    }
    //    if (musicSource.isPlaying)
    //    {
    //        musicSource.Stop();
    //    }
    //    musicSource.clip = gameOverTheme;
    //    musicSource.Play();
    //}
    //public void PlaySFX(AudioClip audio)
    //{
    //    sfxSource.PlayOneShot(audio);
    //}
}