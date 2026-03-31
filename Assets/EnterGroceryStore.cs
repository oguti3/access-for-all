using System.Collections;
using UnityEngine;

public class EnterGroceryStore : MonoBehaviour
{
    [SerializeField] Transform teleportDestination;
    [SerializeField] AudioClip newAudio;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(TeleportWithDelay(other.transform));
        }
    }

    IEnumerator TeleportWithDelay(Transform player)
    {
        yield return new WaitForSeconds(1f); // wait
        if (player != null)
        {
            player.transform.position = teleportDestination.position;
            player.transform.rotation = teleportDestination.rotation;
        }
        //AudioManager.instance.ChangeAudio(newAudio, 0.5f); // stop old audio immediately
    }
}