using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource mainTheme;
    private float fadeTime = 3f;


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


    // Start is called before the first frame update
    void Start()
    {
      mainTheme.Play();
    }



    public void FadeOutTrigger()
    {

        print("working");
        StartCoroutine(FadeOut());
    }
   
    public  IEnumerator FadeOut()
    {
        float currentVolume = mainTheme.volume;
        print("music ending");
        while (mainTheme.volume > 0)
        {
            mainTheme.volume -= currentVolume * Time.deltaTime / fadeTime;

            yield return null;
        }

        mainTheme.Stop();
        mainTheme.volume = currentVolume;

        yield return null;
    }

  

   public  IEnumerator FadeIn()
   {
       float startVolume = 0.2f;

       mainTheme.volume = 0;
       mainTheme.Play();

       while (mainTheme.volume < 1.0f)
       {
           mainTheme.volume += startVolume * Time.deltaTime / fadeTime;

           yield return null;
       }

       mainTheme.volume = 1f;
   }

  
}
