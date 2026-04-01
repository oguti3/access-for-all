using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbleistMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float moveDistance = 4.5f;

    public MoveShelf1 shelfScript;

    private bool shouldMove = false;
    private bool limitReached = false;
    Vector3 originalPosition;
     

    public float rotationSpeed = 90f; // degrees per second
    private Vector3 targetPosition;
    private Quaternion targetRotation;

    void Start() {
        shelfScript = FindObjectOfType<MoveShelf1>();
        originalPosition = transform.position;
        targetPosition = originalPosition + new Vector3(0f, 0f, moveDistance);
        targetRotation = transform.rotation * Quaternion.Euler(90f, 0f, 0f);
    }


    void Update()
    {
        if (shelfScript == null)
        {
            Debug.Log("shelfScript is NULL");
            return;
        }

   

        if (shelfScript.hasMoved)
        {
            shouldMove = true;
        }

        if (shouldMove && !limitReached)
        {
            // Move
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );

            // Rotate
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );

            // Check if BOTH done
            bool reachedPosition = transform.position == targetPosition;
            bool reachedRotation = Quaternion.Angle(transform.rotation, targetRotation) < 0.1f;

            if (reachedPosition && reachedRotation)
            {
                if (transform.childCount > 0)
                {
                    Transform child = transform.GetChild(0);
                    child.SetParent(null);
                    child.gameObject.SetActive(true);
                }

                gameObject.SetActive(false);
                limitReached = true;
            }
        }
    }    
}