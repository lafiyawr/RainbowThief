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
       
        var newPos = Camera.main.transform.TransformPoint(0 ,10f, 10f);
        var newRot = Camera.main.transform.rotation;

        confetti.transform.position = newPos;
       // confetti.transform.rotation = newRot;
        confetti.SetActive(true);
    }
    
}
