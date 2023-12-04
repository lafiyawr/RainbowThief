using UnityEngine;
using UnityEngine.SceneManagement;

public class Launcher : MonoBehaviour
{
    public GameObject _prefab;
 

    public void ShootingStar()
    {
        print("shoot");
        var pos = Camera.main.transform.position;
        var forw = Camera.main.transform.forward;
        var rot = Camera.main.transform.rotation;
        var thing = Instantiate(_prefab, pos + (forw * 0.1f), rot);

        //if it has physics fire it!
        if (thing.TryGetComponent(out Rigidbody rb))
        {
            rb.AddForce(forw * 800.0f);
        }

    }

}
