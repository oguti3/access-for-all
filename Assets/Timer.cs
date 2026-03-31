using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshPro timerText;
    [SerializeField] float remainingTime = 30f;
    public bool countingUp = false;
    [SerializeField] float infinityThreshold = 40f;
    public bool isInf = false;

    public UnityEvent OnTimerEnd;

    private bool hasEnded = false;

    void Update()
    {
        if (isInf) return;

        if (countingUp)
        {
            remainingTime += Time.deltaTime;
        }
        else if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else
        {
            if (!hasEnded)
            {
                hasEnded = true;
                remainingTime = 0;
                OnTimerEnd?.Invoke();
            }
        }

        if (remainingTime >= infinityThreshold)
        {
            timerText.text = "\u221E";
            timerText.fontSize = 100f;
            isInf = true;
        }
        else
        {
            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}