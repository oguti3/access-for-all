using UnityEngine;

public class MoveAlly : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float moveDistance = 4.5f;
    public float rotationSpeed = 90f;
    public float rotationAmount = 90f;
    private bool limitReached = false;

    public Vector3 targetPosition;
    public Vector3 targetEulerRotation;
    public AbleistToAlly ableist_to_ally;
    private Quaternion targetRotation;

    void Start()
    {
        targetRotation = Quaternion.Euler(targetEulerRotation);
    }

    void Update()
    {
        if (ableist_to_ally.changed && !limitReached)
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
                    limitReached = true;
                }
            }
        }
    }
}