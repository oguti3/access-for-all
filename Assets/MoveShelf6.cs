using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveShelf6 : CAVE2Interactable
{
    int count = 0;
    bool hasMoved = false;
    bool isPointing = false; // back to manual flag

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
                transform.position -= new Vector3(4.8f, 0f, 0f);
                hasMoved = true;
            }
        }
        else
        {
            count = 0;
        }

        isPointing = false; // reset every frame
    }

    public new void OnWandPointing(CAVE2.WandEvent playerInfo)
    {
        base.OnWandPointing(playerInfo);
        isPointing = true; // only true if ray hits shelf this frame
    }
}