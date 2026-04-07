using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPerson : MonoBehaviour
{
    public MoveShelf6 shelfScript;
    public GameObject positiveText;
    public GameObject negativeText;
    // Start is called before the first frame update
    void Start()
    {
        if(shelfScript.hasMoved == false)
        {
            gameObject.SetActive(false);
            positiveText.gameObject.SetActive(false);
            negativeText.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (shelfScript.hasMoved)
        {
            gameObject.SetActive(true);
            positiveText.gameObject.SetActive(true);
            negativeText.gameObject.SetActive(true);
        }
    }
}
