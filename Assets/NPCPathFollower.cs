using UnityEngine;

public class NPCPathFollower : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 3f;
    public float reachDistance = 0.2f;

    private int currentWaypointIndex = 1;

    void Update()
    {
        if (waypoints.Length == 0) return;

        Transform target = waypoints[currentWaypointIndex];
        Vector3 direction = (target.position - transform.position).normalized;

        transform.position += direction * speed * Time.deltaTime;

        // Rotate toward movement direction
        if (direction != Vector3.zero)
        {
            transform.forward = direction;
            transform.Rotate(0, 0, 90);
        }

        // Check if reached waypoint
        if (Vector3.Distance(transform.position, target.position) < reachDistance)
        {
            currentWaypointIndex++;

            if (currentWaypointIndex >= waypoints.Length)
            {
                transform.position = waypoints[0].position;
                currentWaypointIndex = 0;
            }
        }
    }
}