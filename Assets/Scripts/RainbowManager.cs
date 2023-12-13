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
    private GameObject _currentShard;
    public AudioSource rainbowShardGlow;
    public AudioSource rainbowShardClick;

    private void OnEnable()
    {
        BossHealth.onBossHurt += RainbowShard;
    }

    private void OnDisable()
    {
        BossHealth.onBossHurt -= RainbowShard;
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

                    //When the Rainbow shard is tapped, stop the music, add a piece to the counter, destroy the shard, and restart the timeline. 
                    if (rayHit.collider.tag == rainbowNames[rainbowTracker])
                    {
                        rainbowShardGlow.Stop();
                        rainbowShardClick.Play();
                        rainbowCounter[rainbowTracker].SetActive(true);
                        _currentShard = GameObject.FindGameObjectWithTag(rainbowNames[rainbowTracker]);
                        //  print(_currentShard.name);
                        TimelineControl.StartTimeline(_playableDirector);
                        // _currentShard.SetActive(false);
                        Destroy(_currentShard);
                        rainbowTracker++;
                        _rainbowEnabled = false;

                    }
                }
            }

        }


    }


    //This gets called when the boss is defeated. Spawns in front of the camera and restarts the timeline
    public void RainbowShard()
    {
        ObjectSpawner.SpawnObject(rainbowPrefabs[rainbowTracker], distance);
        rainbowShardGlow.Play();
        _rainbowEnabled = true;
        TimelineControl.StartTimeline(_playableDirector);


    }



}
