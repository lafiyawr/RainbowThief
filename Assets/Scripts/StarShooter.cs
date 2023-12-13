using UnityEngine;

public class StarShooter : MonoBehaviour
{
    public GameObject _prefab;
    public AudioSource click;


    //This is attached to the "StarShooter" gameobject. It gets called OnClick. 
    public void ShootingStar()
    {

        var pos = Camera.main.transform.position;
        var forw = Camera.main.transform.forward;
        var rot = Camera.main.transform.rotation;
        var starPrefab = Instantiate(_prefab, pos + (forw * 0.1f), rot);
        click.Play();

        if (starPrefab.TryGetComponent(out Rigidbody rb))
        {
            rb.AddForce(forw * 800.0f);
        }

        Destroy(starPrefab, 3f);

    }

}
