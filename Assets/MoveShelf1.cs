using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveShelf1 : CAVE2Interactable
{
    int count = 0;
    public bool hasMoved;
    bool isPointing, startMovement = false; // back to manual flag
    Vector3 normalPosition;
    WandPointer wandPointer;

    void Start()
    {
        normalPosition = transform.position;
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

        if (wandPointer.laserActivated && isPointing)
        {
            startMovement = true;
        }

        if (startMovement && !hasMoved)
        {
            if (count < 10)
            {
                count++;
               // Debug.Log(gameObject.name + " count: " + count);
            }
            else
            {
                //Debug.Log(gameObject.name + " MOVING NOW");

                if (transform.position.z >= normalPosition.z) 
                {
                    hasMoved = true;
                }

                transform.position += new Vector3(0f, 0f, 2f * Time.deltaTime);
                
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