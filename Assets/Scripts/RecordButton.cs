using Niantic.Lightship.AR.Scanning;
using UnityEngine;

public class RecordButton : MonoBehaviour
{
    // enable the scanning manager to start recording
    public ARScanningManager _scanningManager;
    public void StartRecording()
    {
        _scanningManager.enabled = true;
    }

}