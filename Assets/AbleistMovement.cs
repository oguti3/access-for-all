using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbleistMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float moveDistance = 5f;

    public MoveShelf1 shelfScript;

    private bool shouldMove = false;
    private bool limitReached = false;

    void Start() {
        shelfScript = FindObjectOfType<MoveShelf1>();
    }


    void Update()
    {
        if (shelfScript == null)
        {
            Debug.Log("shelfScript is NULL");
            return;
        }
        if (shelfScript == null) return;

        Debug.Log("ShelfPosition-Z: " + shelfScript.transform.position.z);

        if (shelfScript.transform.position.z >= -0.04f)
        {
            shouldMove = true;
        }

        if (shouldMove && !limitReached)
        {
            Debug.Log("Reached");
            transform.position += new Vector3(0f, 0f, 0.01f);
            if (transform.position.z >= -2)
            {
                limitReached = true;
            }

        }

        /*if (limitReached) {
            
        }*/

    }    
}




/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AbleistMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float moveDistance = 5f; // how far right they go

    public MoveShelf6 shelfScript;

    private Vector3 shelfCurrPos;
    private Vector3 startPos;

    private bool shouldMove = false;
    private bool limitReached = false;



    private Vector3 shelfStartPos;

    void Start()
    {
        shelfCurrPos = GameObject.FindWithTag("MoveableShelves").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("ShelfPosition-Z: " + shelfCurrPos.z);
        if (shelfScript.transform.position.z >= -1f)
        {
            shouldMove = true;
        }


        // move right after shelf moved
        if (shouldMove && !limitReached)
        {
            Debug.Log("Reached!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            transform.position += new Vector3(0f, 0f, 3.56f);
            limitReached = true;
        }
    }
}*/