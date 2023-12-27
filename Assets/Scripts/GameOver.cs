using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject confetti;
    public float distance;


   // A simple script to make sure the confetti shows up in the right place when the game ends. :)
    public void WinGame()
    {
        ObjectSpawner.SpawnObject(confetti, distance);
        confetti.SetActive(true);
    }
    
}
