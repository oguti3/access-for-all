using UnityEngine;

public class EmotionClash : MonoBehaviour
{
    public string targetTag;
    public int maxCount = 5;
    public GameObject invisibleWall;
    public GameObject person;
    public GrabbableObject grabbable;   // assign in Inspector

    int count = 0;
    Vector3 startPosition;
    Quaternion startRotation;
    Rigidbody rb;
    bool lastGrabbedState;

    void Awake()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
        rb = GetComponent<Rigidbody>();

        rb.isKinematic = true;
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.None;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    void Update()
    {
        if (grabbable == null) return;

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
        }
        lastGrabbedState = currentGrabbed;
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag(targetTag))
        {
            count++;

            if (count >= maxCount)
            {
                // Clash successful
                collision.gameObject.SetActive(false);
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                gameObject.SetActive(false);

                if (invisibleWall != null) invisibleWall.SetActive(false);
                if (person != null)
                {
                    Transform child = transform.GetChild(0);
                    child.SetParent(null);
                    child.gameObject.SetActive(true);
                    person.SetActive(false);
                }
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag(targetTag))
        {
            if (count < maxCount)
            {
                // Clash failed – reset and freeze
                rb.isKinematic = true;

                transform.position = startPosition;
                transform.rotation = startRotation;

                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            count = 0;
        }
    }

    public void OnGrabbed()
    {
        rb.isKinematic = false;
        rb.constraints = RigidbodyConstraints.None;
    }
}