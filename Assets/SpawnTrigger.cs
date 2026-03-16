using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject objectToSpawn;       // Assign your prefab in the Inspector
    public Transform spawnPoint;           // Where to spawn (optional, uses trigger center if null)
    public bool spawnOnce = true;          // Prevent repeated spawning

    private bool hasSpawned = false;

    void OnTriggerEnter(Collider other)
    {
        // Only react to the Player
        if (!other.CompareTag("Player")) return;

        if (spawnOnce && hasSpawned) return;

        SpawnObject();
    }

    void SpawnObject()
    {
        if (objectToSpawn == null)
        {
            Debug.LogWarning("No prefab assigned to SpawnTrigger!");
            return;
        }

        Vector3 position = spawnPoint != null ? spawnPoint.position : transform.position;
        Quaternion rotation = spawnPoint != null ? spawnPoint.rotation : Quaternion.identity;

        Instantiate(objectToSpawn, position, rotation);
        hasSpawned = true;
    }
}