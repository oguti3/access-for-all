using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveShelf6 : CAVE2Interactable
{
    int count = 0;
    public bool hasMoved;
    bool isPointing, startMovement = false; // back to manual flag
    public GameObject useless;
    public GameObject problem;
    public GameObject fix;
    public GameObject obstacle;
    WandPointer wandPointer;
    Vector3 normalPosition;
    public GameObject invisibleWall;

    void Start()
    {
        if (useless != null)
        {
            useless.gameObject.SetActive(false);
        }
        if (problem != null)
        {
            problem.gameObject.SetActive(false);
        }
        if (fix != null)
        {
            fix.gameObject.SetActive(false);
        }
        if (obstacle != null)
        {
            obstacle.gameObject.SetActive(false);
        }
        if (invisibleWall != null)
        {
            invisibleWall.gameObject.SetActive(true);
        }
        normalPosition = transform.position;
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
                if (transform.position.x <= normalPosition.x)
                {
                    useless.gameObject.SetActive(true);
                    problem.gameObject.SetActive(true);
                    fix.gameObject.SetActive(true);
                    obstacle.gameObject.SetActive(true);
                    invisibleWall.gameObject.SetActive(false);
                    hasMoved = true;
                } else
                {
                    transform.position -= new Vector3(2f * Time.deltaTime, 0f, 0f);
                }

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