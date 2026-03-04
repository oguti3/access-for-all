using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionClash : MonoBehaviour
{
    private Transform player;
    private Vector3 offset;
    public float maxDistance = 0.5f;
    public float minDistance = 0.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        offset = transform.position - player.position;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > maxDistance)
        {
            Vector3 direction = (transform.position - player.position).normalized;
            transform.position = player.position + direction * maxDistance;
        }
        else if (distance < minDistance)
        {
            Vector3 direction = (transform.position - player.position).normalized;
            transform.position = player.position + direction.normalized * minDistance;
        }
        Vector3 pos = transform.position;
        float minY = player.position.y - 1f; 
        float maxY = player.position.y + 0.5f; 
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Negative"))
        {
            collision.gameObject.SetActive(false);
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
            gameObject.SetActive(false);
            transform.position = player.position + offset;
        
            rb.isKinematic = false;
            gameObject.SetActive(true);
        }
    }
}
