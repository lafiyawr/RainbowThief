using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class BWFade : MonoBehaviour
{



    private Volume _bwVolume;
    private ColorAdjustments _bwgrading;
    float timeElapsed;
    public float lerpDuration = 5;
    float startValue = 0;
    float endValue = -100;
    public bool startFade = false;
    public bool endFade = false;
    void Start()
    {
        var getVolume = GetComponent<Volume>();
        _bwVolume = getVolume;
        _bwVolume.profile.TryGet<ColorAdjustments>(out _bwgrading);

    }


    void Update()
    {

        if (startFade)
        {
            // This triggers the post processing color saturation to fade to black and white after a specified duration.
            if (timeElapsed < lerpDuration)
            {
                _bwgrading.saturation.value = Mathf.Lerp(startValue, endValue, timeElapsed / lerpDuration);
                timeElapsed += Time.deltaTime;
                //  print(timeElapsed);

            }
            else
            {
                startFade = false;
                return;
            }

        }

        if (endFade)
        {
            // This trigger the post processing color saturation to fade back to color after a specified duration.
            if (timeElapsed < lerpDuration)
            {
                _bwgrading.saturation.value = Mathf.Lerp(endValue, startValue, timeElapsed / lerpDuration);
                timeElapsed += Time.deltaTime;
                //  print(timeElapsed);

            }
            else
            {
                endFade = false;
                return;
            }

        }



    }

    public void FadeStart()
    {
        startFade = true;
    }

    public void FadeEnd()
    {
        timeElapsed = 0;
        endFade = true;
    }
}
