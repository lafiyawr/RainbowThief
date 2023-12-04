using Niantic.Lightship.AR.Semantics;
using Niantic.Lightship.AR.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class SkySpawn1 : MonoBehaviour
{
    public ARCameraManager _cameraMan;
    public ARSemanticSegmentationManager _semanticMan;
    public AROcclusionManager _occMan;
    public GameObject _prefabToSpawn;
    public TMP_Text _text;
    public RawImage _image;
    public Material _material;
    public float distance = 0f;
    public bool spawnRainbow = false;
    public GameObject rainbow;
    public Camera _camera;
    [SerializeField]
    private PlayableDirector _playableDirector;

    private string _channel = "sky";
    XRCpuImage? depthimage;
    void OnEnable()
    {
        _cameraMan.frameReceived += OnCameraFrameUpdate;
    }

    private void OnDisable()
    {
        _cameraMan.frameReceived -= OnCameraFrameUpdate;
    }




    private void OnCameraFrameUpdate(ARCameraFrameEventArgs args)
    {
        if (!_semanticMan.subsystem.running)
        {
            return;
        }

        //get the semantic texture
        Matrix4x4 mat = Matrix4x4.identity;
        var texture = _semanticMan.GetSemanticChannelTexture(_channel, out mat);

        if (texture)
        {
            //the texture needs to be aligned to the screen so get the display matrix
            //and use a shader that will rotate/scale things.
            Matrix4x4 cameraMatrix = args.displayMatrix ?? Matrix4x4.identity;
            _image.material = _material;
            _image.material.SetTexture("_SemanticTex", texture);
            _image.material.SetMatrix("_SemanticMat", mat);
        }
    }



    private float _timer = 0.0f;

    // Update is called once per frame
    void Update()
    {
        if (!_semanticMan.subsystem.running && !_occMan.subsystem.running)
        {
            return;
        }

        Matrix4x4 displayMat = Matrix4x4.identity;

        if (_occMan.TryAcquireEnvironmentDepthCpuImage(out var image))
        {
            depthimage?.Dispose();
            depthimage = image;
        }
        else
        {
            return;
        }

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            var pos = Input.mousePosition;
            //depth position
            //  var screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            print("TOUCHING");
#else
       if (Input.touches.Length > 0)
            {
            var pos = Input.touches[0].position;
          
#endif
            if (pos.x > 0 && pos.x < Screen.width)
                if (pos.y > 0 && pos.y < Screen.height)
                {
                    _timer += Time.deltaTime;


                    if (_timer > 0.05f)
                    {
                        var list = _semanticMan.GetChannelNamesAt((int)pos.x, (int)pos.y);

                        if (list.Count > 0 && list[0] == _channel && !spawnRainbow)
                        {
                            if (depthimage.HasValue)
                            {
                                // Sample eye depth
                                var uv = new Vector2(pos.x / Screen.width, pos.y / Screen.height);
                                uv = new Vector2(0, 0);
                                var eyeDepth = depthimage.Value.Sample<float>(uv, displayMat);

                                // Get world position
                                var worldPosition =
                                    _camera.ScreenToWorldPoint(new Vector3(pos.x, pos.y, eyeDepth + distance));
                                _playableDirector.Play();
                                _text.text = "sky!";

                                //spawn a thing on the depth map
                                Instantiate(_prefabToSpawn, worldPosition, _camera.transform.rotation);
                                spawnRainbow = true;

                            }

                        }
                        else
                        {
                            _channel = list[0];
                            _text.text = _channel;
                        }

                        _timer = 0.0f;
                    }
                }





        }



    }

}
