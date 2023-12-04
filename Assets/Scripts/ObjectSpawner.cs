using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class ObjectSpawner 
{
  
    public static void SpawnObject(GameObject obj, float distance)
    {
        var newPos = Camera.main.transform.TransformPoint(Vector3.forward * distance);
        var newRot = Camera.main.transform.rotation;
        UnityEngine.Object.Instantiate(obj, newPos, newRot);

    }


}
