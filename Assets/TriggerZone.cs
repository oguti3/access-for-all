using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    public AbleistMovement2 ableistMovement;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ableistMovement.shouldMove = true;
        }
    }
}