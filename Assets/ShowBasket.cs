using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowBasket : MonoBehaviour
{
    MoveShelf1 shelf1;
    bool isShowing = false;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        shelf1 = GetComponent<MoveShelf1>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isShowing && shelf1.hasMoved)
        {
            gameObject.SetActive(true);
        }
    }
}
