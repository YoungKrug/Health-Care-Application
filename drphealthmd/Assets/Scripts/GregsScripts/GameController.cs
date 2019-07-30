using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject playArea;
    public GameObject placer;
    public GameObject objectToSpawnOnCard;
    public GameObject cardArea;
    public Button button;
    bool isWaitingForPlayerInput = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isWaitingForPlayerInput)
        {
            // Make the outline of the cubes
            // Check if card is in the area
            placer.SetActive(false);
            // Now we have to track for playerInput for objects
            // We need a way to track playerMovement... Oh -Look Below
            // It will be connected to a server and wait for the teachers input to let us know
            // 


        }
    }
    //When someone clicks the button we have the player input
    public void SetIsWaitingForPlayerInput()
    {
        isWaitingForPlayerInput = false;
    }
}
