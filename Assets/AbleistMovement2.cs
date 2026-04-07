using UnityEngine;

public class AbleistMovement2 : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float moveDistance = 4.5f;
    public float rotationSpeed = 90f;
    public float rotationAmount = 90f;
    public bool shouldMove = false;
    private bool limitReached = false;

    public Vector3 targetPosition;
    public Vector3 targetEulerRotation;
    private Quaternion targetRotation;

    public GameObject a1;
    public GameObject a2;
    public GameObject a3;
    public GameObject a4;
    public GameObject aw1;
    public GameObject p1;
    public GameObject p2;
    public GameObject p3;
    public GameObject p4;

    void Start()
    {
        a1.gameObject.SetActive(false);
        a2.gameObject.SetActive(false);
        a3.gameObject.SetActive(false);
        a4.gameObject.SetActive(false);
        aw1.gameObject.SetActive(false);
        p1.gameObject.GetComponent<AbleistToAlly>().enabled = false;
        p2.gameObject.GetComponent<AbleistToAlly>().enabled = false;
        p3.gameObject.GetComponent<AbleistToAlly>().enabled = false;
        p4.gameObject.GetComponent<AbleistToAlly>().enabled = false;
        targetRotation = Quaternion.Euler(targetEulerRotation);
    }

    void Update()
    {
        if (shouldMove && !limitReached)
        {
            bool reachedPosition = Vector3.Distance(transform.position, targetPosition) < 0.01f;

            if (!reachedPosition)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position, targetPosition, moveSpeed * Time.deltaTime);
            }
            else
            {
                transform.position = targetPosition; // Snap exactly to avoid drift

                transform.rotation = Quaternion.RotateTowards(
                    transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

                if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
                {
                    a1.gameObject.SetActive(true);
                    a2.gameObject.SetActive(true);
                    a3.gameObject.SetActive(true);
                    a4.gameObject.SetActive(true);
                    aw1.gameObject.SetActive(true);
                    p1.gameObject.GetComponent<AbleistToAlly>().enabled = true;
                    p2.gameObject.GetComponent<AbleistToAlly>().enabled = true;
                    p3.gameObject.GetComponent<AbleistToAlly>().enabled = true;
                    p4.gameObject.GetComponent<AbleistToAlly>().enabled = true;
                    limitReached = true;
                }
            }
        }
    }
}