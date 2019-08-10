using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontMove : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 pos;
    void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Using this script to ensure the play area does not move in an AR space
        //If this is false then we are not looking for player input, lock the position
        if (!GameObject.Find("GameController").GetComponent<GameController>().isWaitingForPlayerInput)
        {
            if (transform.position != pos)
            {
              //  transform.position = pos;
            }
        }
    }
}
