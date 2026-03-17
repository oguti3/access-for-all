using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGrocery : CAVE2Interactable
{
    bool isHovered = false;
    int count = 0;
    bool hasMoved = false;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        transform.position += new Vector3(2.46f, 0f, 0f);

        SetStatic(); // start as static and phaseable
    }

    void Update()
    {
        if (isHovered && !hasMoved)
        {
            SetPhysicsActive(); // enable collisions when hovered

            if (count < 3)
            {
                count++;
            }
            else
            {
                transform.position -= new Vector3(2.46f, 0f, 0f);
                hasMoved = true;
            }
        }
        else if (!isHovered)
        {
            count = 0;
            SetStatic(); // back to phaseable when not hovered
        }

        isHovered = false;
    }

    void SetStatic()
    {
        GetComponent<Collider>().enabled = false;

        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    void SetPhysicsActive()
    {
        GetComponent<Collider>().enabled = true;

        if (rb != null)
        {
            rb.isKinematic = true;  // kinematic so grocery doesn't fall off shelf
            rb.useGravity = false;
        }
    }

    public new void OnWandPointing(CAVE2.WandEvent playerInfo)
    {
        base.OnWandPointing(playerInfo);
        isHovered = true;
    }
}