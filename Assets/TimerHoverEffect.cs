using UnityEngine;
using TMPro;

public class TimerHoverEffect : CAVE2Interactable
{
    [SerializeField] Color normalColor = Color.white;
    [SerializeField] Color hoverColor = Color.yellow;
    [SerializeField] float hoveredFontSize = 36f;

    TextMeshPro timerText;
    WandPointer wandPointer;
    Timer timer;
    float normalFontSize;
    string normalText;
    bool isHovered = false;

    void Start()
    {
        timerText = GetComponent<TextMeshPro>();
        timerText.faceColor = normalColor;
        timerText.alignment = TextAlignmentOptions.Center;

        // Save originals so we can restore them
        normalFontSize = timerText.fontSize;
        Debug.Log("Normal font size: " + timerText.fontSize);
        normalText = timerText.text;
        timer = GetComponent<Timer>();
        wandPointer = FindObjectOfType<WandPointer>();
        if (wandPointer == null)
            Debug.LogError("WandPointer not found!");
        else
            Debug.Log("WandPointer found on: " + wandPointer.gameObject.name);
    }

    public new void OnWandPointing(CAVE2.WandEvent playerInfo)
    {
        base.OnWandPointing(playerInfo);
        isHovered = true;
    }

    void Update()
    {
        if (wandPointer == null || timer == null || timer.isInf) return;

        if (wandPointer.laserActivated && isHovered)
        {
            timer.countingUp = true;
            timerText.faceColor = hoverColor;
            timerText.fontSize = hoveredFontSize;
        }
        else
        {
            timer.countingUp = false;
            timerText.faceColor = normalColor;
            timerText.fontSize = normalFontSize;
        }

        timerText.alignment = TextAlignmentOptions.Center;
        isHovered = false; // reset every frame
    }
}