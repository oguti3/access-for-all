using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionClash : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Negative"))
        {
            collision.gameObject.SetActive(false);
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;

            rb.isKinematic = false;
            gameObject.SetActive(false);
        }
    }
}