using UnityEngine;
using TMPro;

public class TimerHoverEffect : MonoBehaviour
{
    [SerializeField] Color normalColor = Color.white;
    [SerializeField] Color hoverColor = Color.yellow;
    [SerializeField] float hoveredFontSize = 36f;

    TextMeshPro timerText;
    Timer timerScript;
    bool isHovered = false;

    void Start()
    {
        timerText = GetComponent<TextMeshPro>();
        timerScript = GetComponent<Timer>();
        timerText.color = normalColor;
        timerText.alignment = TextAlignmentOptions.Center;
    }

    void OnWandPointing(CAVE2.WandEvent playerInfo)
    {
        isHovered = true;
    }

    void Update()
    {
        if (isHovered)
        {
            timerScript.enabled = false;
            timerText.color = hoverColor;
            timerText.text = "\u221E";
            timerText.fontSize = hoveredFontSize;
            timerText.alignment = TextAlignmentOptions.Center;
        }
    }
}