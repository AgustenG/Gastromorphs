using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SplashScreen : MonoBehaviour
{
    public float waitTime;
    public VideoPlayer video;
    void Start()
    {
        StartCoroutine(WaitForIntro());
    }

    IEnumerator WaitForIntro()
    {
        video.Play();
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(1);
    }
}
