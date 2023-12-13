using UnityEngine.Playables;

public static class TimelineControl
{

    public static void PauseTimeline(PlayableDirector _playableDirector)
    {
        _playableDirector.Pause();
    }

    public static void StartTimeline(PlayableDirector _playableDirector)
    {
        _playableDirector.Play();
    }



}
