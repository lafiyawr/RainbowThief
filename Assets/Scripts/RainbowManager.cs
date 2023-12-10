using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class RainbowManager : MonoBehaviour
{

    public GameObject[] rainbowCounter;
    public GameObject[] rainbowPrefabs;
    public string[] rainbowNames;
    public int rainbowTracker = 0;
    int distance = 2;
    [SerializeField]
    private PlayableDirector _playableDirector;
    public bool _rainbowEnabled = false;
    public GameObject bossMap;
    GameObject rainbowShard;
    private GameObject currentShard;
    public AudioSource rainbowShardGlow;
    public AudioSource rainbowShardClick;


 



    private void OnEnable()
    {
        health.onBossHurt += RainbowShard;
    }

    private void OnDisable()
    {
        health.onBossHurt -= RainbowShard;
    }



    public void Update()
    {

        if (_rainbowEnabled)
        {
            print("enabled!");
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit rayHit;
                if (Physics.Raycast(ray, out rayHit, 100.0f))
                {
                    if (rayHit.collider.tag == rainbowNames[rainbowTracker]) 
                    {
                      rainbowShardGlow.Stop();
                    rainbowShardClick.Play();
                        rainbowCounter[rainbowTracker].SetActive(true);
                     currentShard = GameObject.FindGameObjectWithTag(rainbowNames[rainbowTracker]);
                        print(currentShard.name);
                        TimelineControl.StartTimeline(_playableDirector);
                        //fallback in case it doesn't destroy like it's supposed to. :\
                        currentShard.SetActive(false);
                        Destroy(currentShard);
                        rainbowTracker++;
                        _rainbowEnabled = false;

                    }
                }
            }

        }
       

    }



    public void RainbowShard()
    {

      
        ObjectSpawner.SpawnObject(rainbowPrefabs[rainbowTracker], distance);
        rainbowShardGlow.Play();
        _rainbowEnabled = true;
      TimelineControl.StartTimeline(_playableDirector);
     
     
    }

    



  


}
