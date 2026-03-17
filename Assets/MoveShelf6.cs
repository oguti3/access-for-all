using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveShelf6 : CAVE2Interactable
{
    int count = 0;
    bool hasMoved, isPointing, startMovement = false; // back to manual flag

    WandPointer wandPointer;

    void Start()
    {
        transform.position += new Vector3(4.8f, 0f, 0f);
        wandPointer = FindObjectOfType<WandPointer>();

        if (wandPointer == null)
            Debug.LogError("WandPointer not found!");
        else
            Debug.Log("WandPointer found on: " + wandPointer.gameObject.name);
    }

    void Update()
    {
        UpdateWandOverTimer(); // keeps CAVE2Interactable happy

        if (wandPointer == null) return;

        if (wandPointer.laserActivated && isPointing)
        {
            startMovement = true;
        }

        if (startMovement && !hasMoved)
        {
            if (count < 10)
            {
                count++;
                //Debug.Log(gameObject.name + " count: " + count);
            }
            else
            {
                //Debug.Log(gameObject.name + " MOVING NOW");

                if (transform.position.x <= -22.7f)
                {
                    hasMoved = true;
                }

                transform.position -= new Vector3(0.01f, 0f, 0f);

            }
        }
        else
        {
            count = 0;
        }

        isPointing = false; // reset every frame
    }

    void OnWandPointing(CAVE2.WandEvent playerInfo)
    {
        isPointing = true; // only true if ray hits shelf this frame
    }
}