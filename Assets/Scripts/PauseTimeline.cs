using UnityEngine;
using UnityEngine.Playables;

public class PauseTimeline : MonoBehaviour
{

    [SerializeField]
    private PlayableDirector _playableDirector;



    public void PauseScene()
    {
        TimelineControl.PauseTimeline(_playableDirector);
    }

}
