using Niantic.Lightship.AR.Loader;
using Niantic.Lightship.AR.LocationAR;
using Niantic.Lightship.AR.PersistentAnchors;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;


public class VpsManager : MonoBehaviour

{
    [SerializeField]
    private ARLocationManager _arLocationManager;

    [SerializeField]
    private TMP_Text _AnchorTrackingStateText;

    private ARLocation[] _arLocation;
    [SerializeField]
    private PlayableDirector _playableDirector;

    public delegate IEnumerator LocationTracking();
    public static event LocationTracking locationTracking;

    public delegate IEnumerator LocationFound();
    public static event LocationFound locationFound;

    private void OnEnable()
    {
        _arLocationManager.locationTrackingStateChanged += OnLocationTrackingStateChanged;
    }

    private void OnDisable()
    {
        _arLocationManager.locationTrackingStateChanged -= OnLocationTrackingStateChanged;
    }

    void Start()
    {
        if (string.IsNullOrWhiteSpace(LightshipSettings.Instance.ApiKey))
        {
            if (_AnchorTrackingStateText != null)
            {
                _AnchorTrackingStateText.text = "No API key is set";
            }

            return;
        }

        if (_arLocationManager == null)
        {
            if (_AnchorTrackingStateText != null)
            {
                _AnchorTrackingStateText.text = "No Location Manager to listen to";
            }

            return;
        }
        if (_arLocationManager.ARLocations.Length < 1)
        {
            _AnchorTrackingStateText.text = "Add an AR Location to the AR Location Manager.";
            return;
        }

        if (_AnchorTrackingStateText != null)
        {
            _AnchorTrackingStateText.text = "Select a Location to start tracking";
            //get the list of AR locations
            _arLocation = _arLocationManager.ARLocations;

        }
    }


    private void OnLocationTrackingStateChanged(ARLocationTrackedEventArgs args)
    {
        if (args.Tracking)
        {
            if (_AnchorTrackingStateText != null)
            {
                _AnchorTrackingStateText.text = $"Anchor Tracked";
                StartCoroutine(locationFound());
                //Resume the timeline once the location has been tracked.
                TimelineControl.StartTimeline(_playableDirector);
            }
        }
        else
        {
            if (_AnchorTrackingStateText != null)
            {
                _AnchorTrackingStateText.text = $"Anchor Untracked";
            }
        }
    }

    //This lets you target a specific location and start tracking it. The array number is based on the list of locations in the "AR Location Manager"
    public void locationChanger(int locNum)
    {
        StartCoroutine(locationTracking());
        _arLocationManager.SetARLocations(_arLocation[locNum]);
        _arLocationManager.StartTracking();
        _AnchorTrackingStateText.text = _arLocation[locNum].name;
        //  print(_AnchorTrackingStateText.name);
    }

    //This stops the current tracking so that a new location can be  tracked. You have to stop the tracking in order to track a new location.
    public void resetTracking()
    {
        _arLocationManager.StopTracking();

        _AnchorTrackingStateText.text = "Finding New Location";

    }


}
