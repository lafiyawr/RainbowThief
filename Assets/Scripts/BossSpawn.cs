using UnityEngine;

public class BossSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bossObject;
    public float distance;


    public void spawnBoss()
    {
        ObjectSpawner.SpawnObject(bossObject, distance);
    }
}
