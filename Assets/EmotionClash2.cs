using UnityEngine;

public class EmotionClash2 : MonoBehaviour
{
    public string targetTag;
    public int maxCount = 5;
    public GrabbableObject grabbable;
    public FloatingText floating;
    int count = 0;
    Vector3 startPosition;
    Quaternion startRotation;
    Rigidbody rb;
    bool lastGrabbedState;
    public bool successfulClash;

    void Awake()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
        rb = GetComponent<Rigidbody>();
        successfulClash = false;
        rb.isKinematic = true;
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.None;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    void Update()
    {
        if (successfulClash || grabbable == null)
        {
            return;
        }

        bool currentGrabbed = grabbable.IsGrabbed;

        // Just started being grabbed → unfreeze
        if (!lastGrabbedState && currentGrabbed)
        {
            OnGrabbed();
        }
        if (lastGrabbedState && !currentGrabbed && count < maxCount)
        {
            rb.isKinematic = true;
            transform.position = startPosition;
            transform.rotation = startRotation;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            count = 0;
            floating.shouldFloat = true;
        }
        lastGrabbedState = currentGrabbed;
    }

    void OnCollisionStay(Collision collision)
    {
        Debug.Log(collision.gameObject.CompareTag(targetTag));
        if (!collision.gameObject.CompareTag(targetTag))
        {
            Debug.Log("target tag: " + targetTag);
            Debug.Log("found tag: " + collision.gameObject.tag);
        }
        if (!successfulClash && collision.gameObject.CompareTag(targetTag))
        {
            count++;

            if (count >= maxCount)
            {
                // Clash successful
                Debug.Log("clash successful");
                if (grabbable.IsGrabbed)
                {
                    grabbable.ForceRelease();
                }
                collision.gameObject.SetActive(false);
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                gameObject.SetActive(false);
                successfulClash = true;
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {

        if (!successfulClash && collision.gameObject.CompareTag(targetTag))
        {
            count = 0;
        }
    }

    public void OnGrabbed()
    {
        if (!successfulClash)
        {
            if (floating != null)
            {
                floating.shouldFloat = false;
            }
            rb.isKinematic = false;
            rb.constraints = RigidbodyConstraints.None;
        }
    }
}