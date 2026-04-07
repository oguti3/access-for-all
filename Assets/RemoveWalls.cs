using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveWalls : MonoBehaviour
{
    public AbleistToAlly p1;
    public AbleistToAlly p2;
    public AbleistToAlly p3;
    public AbleistToAlly p4;
    public GameObject w1;
    public GameObject w2;
    public GameObject w3;
    public GameObject w4;
    bool wallsRemoved = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!wallsRemoved && p1.changed && p2.changed && p3.changed && p4.changed)
        {
            Debug.Log("Walls are removed");
            w1.gameObject.SetActive(false);
            w2.gameObject.SetActive(false);
            w3.gameObject.SetActive(false);
            w4.gameObject.SetActive(false);
            wallsRemoved = true;
        }
    }
}
