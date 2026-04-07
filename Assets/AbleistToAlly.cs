using UnityEngine;

public class AbleistToAlly : MonoBehaviour
{
    public GameObject ableism;
    public GameObject ally;
    public bool changed = false;

    void Update()
    {
        if (ableism != null && !ableism.activeInHierarchy)
        {
            gameObject.SetActive(false);
            ally.gameObject.SetActive(true);
            changed = true;
        }
    }
}