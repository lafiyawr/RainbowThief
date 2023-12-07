using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class bwFade : MonoBehaviour
{
    // Start is called before the first frame update


    private Volume bwVolume;
    private ColorAdjustments bwgrading;
    float timeElapsed;
    public float lerpDuration = 5;
    float startValue = 0;
    float endValue = -100;
    public bool startFade = false;
    public bool endFade = false;
    void Start()
    {
        var getVolume = GetComponent<Volume>();
        bwVolume = getVolume;
        bwVolume.profile.TryGet<ColorAdjustments>(out bwgrading);

    }

    // Update is called once per frame
    void Update()
    {

        if (startFade)
        {
            // this trigger the post processing color saturation to fade to black and white after a certain duration.
            if (timeElapsed < lerpDuration)
            {
                bwgrading.saturation.value = Mathf.Lerp(startValue, endValue, timeElapsed / lerpDuration);
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
            // this trigger the post processing color saturation to fade to black and white after a certain duration.
            if (timeElapsed < lerpDuration)
            {
                bwgrading.saturation.value = Mathf.Lerp(endValue, startValue, timeElapsed / lerpDuration);
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

    public void fadeStart()
    {
        startFade = true;
    }

    public void fadeEnd()
    {
        timeElapsed = 0;
        endFade = true;
    }
}
