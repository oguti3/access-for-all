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
                Transform child = transform.GetChild(0); // fix
                child.SetParent(null);
                child.gameObject.SetActive(true); // fix
                gameObject.SetActive(false);
                limitReached = true;
            }

        }

    }    
}