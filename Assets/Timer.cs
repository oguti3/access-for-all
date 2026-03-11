using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshPro timerText;  // TextMeshPro for 3D object
    [SerializeField] float remainingTime;
    float elapsedTime;

    void Update() {
        
        if (remainingTime > 0) {
            remainingTime -= Time.deltaTime;
        }
        else if (remainingTime < 0) {
            remainingTime = 0;
        }
        int minutes = Mathf.FloorToInt(remainingTime / 60); 
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}