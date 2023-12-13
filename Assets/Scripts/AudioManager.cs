using System.Collections;
using UnityEngine;

//This script controls when the background music playsE

public class AudioManager : MonoBehaviour
{
    public AudioSource mainTheme;
    private float _fadeTime = 3f;


    private void OnEnable()
    {
        SemanticManager.skyTapped += FadeOut;
        VpsManager.locationTracking += FadeIn;
        VpsManager.locationFound += FadeOut;
    }

    private void OnDisable()
    {
        SemanticManager.skyTapped -= FadeOut;
        VpsManager.locationTracking -= FadeIn;
        VpsManager.locationFound -= FadeOut;
    }



    void Start()
    {
        mainTheme.Play();
    }



    public void FadeOutTrigger()
    {


        StartCoroutine(FadeOut());
    }

    public IEnumerator FadeOut()
    {
        float currentVolume = mainTheme.volume;
        print("music ending");
        while (mainTheme.volume > 0)
        {
            mainTheme.volume -= currentVolume * Time.deltaTime / _fadeTime;

            yield return null;
        }

        mainTheme.Stop();
        mainTheme.volume = currentVolume;

        yield return null;
    }



    public IEnumerator FadeIn()
    {
        float _startVolume = 0.2f;

        mainTheme.volume = 0;
        mainTheme.Play();

        while (mainTheme.volume < 1.0f)
        {
            mainTheme.volume += _startVolume * Time.deltaTime / _fadeTime;

            yield return null;
        }

        mainTheme.volume = 1f;
    }


}
