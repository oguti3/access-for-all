using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarCollision : MonoBehaviour

{
    public string targetTag;
    public Transform target = null;
    public Vector3 startingPosition;

    void Start()
    {
        startingPosition = transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.CompareTag(targetTag));
        if (!other.gameObject.CompareTag(targetTag))
        {
            Debug.Log("target tag: " + targetTag);
            Debug.Log("found tag: " + other.gameObject.tag);
        }
        if (other.gameObject.tag == "car")
        {
            Debug.Log("collision successful");
            this.transform.position = startingPosition;
        }
    }
}
