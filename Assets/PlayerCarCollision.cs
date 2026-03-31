using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarCollision : MonoBehaviour

{
    public string targetTag;
    public Vector3 startingPosition;

    void Start()
    {
        startingPosition = transform.position;
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag(targetTag))
        {
            this.transform.position = startingPosition;
        }
    }
}
