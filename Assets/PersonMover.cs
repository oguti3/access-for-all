using UnityEngine;
using System.Collections;

/// <summary>
/// Attach to any person mesh. Each person moves forward automatically on Play
/// at their own speed and stops after their set distance.
/// Tweak Speed and Distance per person in the Inspector to position them
/// naturally along the path at the right moment.
/// </summary>
/// 
public class PersonMover : MonoBehaviour
{
    [Header("Movement Settings")]
    [Tooltip("How fast this person moves (units per second)")]
    public float speed = 2f;

    [Tooltip("How far this person walks before stopping (world units)")]
    public float distance = 5f;

    [Tooltip("Delay in seconds before this person starts moving")]
    public float startDelay = 0f;

    [Tooltip("Loop back and forth forever instead of stopping")]
    public bool loop = false;

    void Start()
    {
        StartCoroutine(loop ? MoveLoop() : MoveOnce());
    }

    public void StopMoving()
    {
        StopAllCoroutines();
    }

    private IEnumerator MoveOnce()
    {
        if (startDelay > 0f)
            yield return new WaitForSeconds(startDelay);

        yield return StartCoroutine(Walk(distance));
    }

    private IEnumerator MoveLoop()
    {
        if (startDelay > 0f)
            yield return new WaitForSeconds(startDelay);

        while (true)
        {
            yield return StartCoroutine(Walk(distance));
            yield return StartCoroutine(Walk(-distance)); // walk back
        }
    }

    private IEnumerator Walk(float targetDistance)
    {
        float travelled = 0f;
        float absTarget = Mathf.Abs(targetDistance);
        int direction = targetDistance >= 0 ? 1 : -1;

        while (travelled < absTarget)
        {
            float step = speed * Time.deltaTime;
            transform.position += transform.forward * direction * step;
            travelled += step;
            yield return null;
        }
    }
}