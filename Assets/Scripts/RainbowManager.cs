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
    int distance = 5;
    [SerializeField]
    private PlayableDirector _playableDirector;
    public bool _rainbowEnabled = false;
    public GameObject bossMap;
    GameObject rainbowShard;



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
                    if (rayHit.collider.tag == rainbowNames[rainbowTracker]) //I test tag, but you could what you want..
                    {
                        print("left clicked");
                        rainbowCounter[rainbowTracker].SetActive(true);

                        rayHit.transform.gameObject.SetActive(false);
                        if(rainbowTracker <1)
                        {
                            map();
                        }

                        rainbowTracker++;
                        _rainbowEnabled = false;

                    }
                }
            }

        }
       

    }



    void Addpiece()
    {
       
        spawnRainbow();
        _rainbowEnabled= true;
        _playableDirector.Play();
    }


    public void spawnRainbow()
    {
        var newPos = Camera.main.transform.TransformPoint(Vector3.forward * distance);
        var newRot = Camera.main.transform.rotation;
        if(rainbowTracker== 0)
        {
            Instantiate(rainbowPrefabs[rainbowTracker], newPos, newRot);
         
        } else
        {
            rainbowShard = GameObject.FindGameObjectWithTag(rainbowNames[rainbowTracker]);
            rainbowShard.GetComponent<SpriteRenderer>().enabled = true;
        }
        
    }

    public void map()
    {
        var newPos = Camera.main.transform.TransformPoint(Vector3.forward * distance);
        var newRot = Camera.main.transform.rotation;
        Instantiate(bossMap, newPos, newRot);
    }


    private void OnEnable()
    {
        health.onBossHurt += Addpiece;
    }


}
