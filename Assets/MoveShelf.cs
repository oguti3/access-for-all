using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveShelf : MonoBehaviour
{
    bool isHovered = false;
    int count = 0;
    bool hasMoved = false;

    void Start()
    {
        transform.position -= new Vector3(0f, 0f, 4.06f);
    }

    void Update()
    {
        if (isHovered && !hasMoved)
        {
            if (count < 360)
            {
                count++;
            }
            else
            {
                transform.position += new Vector3(0f, 0f, 4.06f);
                hasMoved = true;
            }
        }
        else if (!isHovered)
        {
            count = 0; // reset when no longer hovering
        }

        isHovered = false; // reset each frame, re-set by OnWandPointing if still hovering
    }

    void OnWandPointing(CAVE2.WandEvent playerInfo)
    {
        isHovered = true;
        Debug.Log(playerInfo);
    }
}