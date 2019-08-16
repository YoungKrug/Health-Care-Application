﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Timeline;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    [HideInInspector]
    public List<GameObject> playArea;
    public List<GameObject> timeLines;
    public List<GameObject> timeLineOneObjects;
    public GameObject placer;
    public GameObject planeFinder;
    public GameObject objectToSpawnOnCard;
    public GameObject cardArea;
    public Image image;
    public Button button;
    public Text text;
    public Text debug;
    public bool isWaitingForPlayerInput = true;
    int numberOfTouches = 0;
    int scenario = 0;
    bool isDoingFirstScenario = false;
    
    void Awake()
    {
        image.gameObject.SetActive(false);
        text.gameObject.SetActive(false);
        button.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(!isWaitingForPlayerInput && !isDoingFirstScenario)
        {
            isDoingFirstScenario = true;
            // Play area
            DontMove[] objs = GameObject.FindObjectsOfType(typeof(DontMove)) as DontMove[];
            for (int i = 0; i < objs.Length; i++)
            {
                playArea.Add(objs[i].gameObject);
            }
            
            button.gameObject.SetActive(false);
            text.gameObject.SetActive(false);
            image.gameObject.SetActive(false);
            PlayScenario();
            // Deactivated the place
            // Make the outline of the cubes
            // Check if card is in the area
            placer.GetComponent<DefaultTrackableEventHandler>().enabled = false;
            planeFinder.SetActive(false);
            //GameObject.FindObjectsOfType(typeof(Playable))
            // Now we have to track for playerInput for objects
            // We need a way to track playerMovement... Oh -Look Below
            // It will be connected to a server and wait for the teachers input to let us know
            // 


        }
        //We know the player has given input, but we have to ensure he wants to use this location
        // int touches = 0;      
        
        if(isWaitingForPlayerInput && numberOfTouches> 0 && placer.GetComponent<DefaultTrackableEventHandler>().checkIfTracking)
            CheckIfObjectHasBeenPlaced();
        DebugText();
        numberOfTouches += Input.touches.Length;
        numberOfTouches += Input.touchCount;
    }
    //Checks if the player has placed the object
    void CheckIfObjectHasBeenPlaced()
    {
        numberOfTouches = 0;
        if (GameObject.FindObjectOfType(typeof(DontMove)))
        {
            //Object has been placed
            button.gameObject.SetActive(true);
            text.gameObject.SetActive(true);
            image.gameObject.SetActive(true);
        }
    }
    //When someone clicks the button we have the player input
    public void SetIsWaitingForPlayerInput()
    {
        isWaitingForPlayerInput = false;
    }
    void PlayScenario()
    {
        //Plays the timeline squences
        if(scenario <= 1)
        {
            SetListEqualToTrue(timeLineOneObjects);
        }
        timeLines[scenario].SetActive(true);
        scenario++;
    }
    void SetListEqualToTrue(List<GameObject> temp)
    {
        foreach(GameObject g in temp)
        {
            g.SetActive(true);
        }
    }
    void DebugText()
    {
        debug.text = placer.GetComponent<DefaultTrackableEventHandler>().checkIfTracking.ToString() + '\n' + Input.touchCount.ToString() + "  " + "\n Number of touches: " + numberOfTouches +
            "\n" + isWaitingForPlayerInput;
    }

}
