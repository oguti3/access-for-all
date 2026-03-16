using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveShelf1 : CAVE2Interactable
{
    int count = 0;
    bool hasMoved = false;
    bool isPointing = false; // back to manual flag

    WandPointer wandPointer;

    void Start()
    {
        transform.position -= new Vector3(0f, 0f, 3.56f);
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

        if (wandPointer.laserActivated && isPointing && !hasMoved)
        {
            if (count < 120)
            {
                count++;
                Debug.Log(gameObject.name + " count: " + count);
            }
            else
            {
                Debug.Log(gameObject.name + " MOVING NOW");
                transform.position += new Vector3(0f, 0f, 3.56f);
                hasMoved = true;
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