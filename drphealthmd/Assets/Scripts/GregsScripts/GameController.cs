using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Timeline;
using UnityEngine.EventSystems;
using UnityEngine.Playables;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    [HideInInspector]
    public List<GameObject> playArea;
    public List<GameObject> timeLines;
    public List<GameObject> timeLineOneObjects;
    public GameObject[] timeLineTwoObjects;
    public GameObject placer;
    public GameObject planeFinder;
    public GameObject objectToSpawnOnCard;
    public GameObject cardArea;
    public GameObject canvasRect;
    public Image image;
    public Button button;
    public Button exerciseComplete;
    public Text text;
    public Text debug;
    public bool isWaitingForPlayerInput = true;
    int numberOfTouches = 0;
    int scenario = 0;
    bool isDoingFirstScenario = false;
    bool cardHasBeenPlaced;
    bool isDone;
    
    void Awake()
    {
       
        image.gameObject.SetActive(false);
        text.gameObject.SetActive(false);
        button.gameObject.SetActive(false);
    }
    private void Start()
    {
        for (int i = 0; i < timeLineTwoObjects.Length; i++)
        {
            timeLineTwoObjects[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Before we continue forward we have to make sure the scene is done.. This will be changed to the festival scene
        if (!isWaitingForPlayerInput && !isDone)
        {
            isDoingFirstScenario = true;
            if(scenario == 2 && !isQuestionTime) // continue
            {
                CardHasBeenPlaced();
            }
            if(scenario != 2)
                HandleScenarioInformation();
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
    void HandleScenarioInformation()
    {
        DontMove[] objs = GameObject.FindObjectsOfType(typeof(DontMove)) as DontMove[];
        if(scenario == 0)
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
    }
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
        if(scenario < 1)
        {
            SetListEqualToTrue(timeLineOneObjects);
            StartCoroutine(Wait());
            timeLines[scenario].SetActive(true);
        }
        if(scenario == 1)
        {
            FlipCardScenario();
            //timeLines[scenario].SetActive(true);
            // return;
        }
        
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
    public void FlipCardScenario()
    {
        //Once certain prereqs have been fullfilled
        text.gameObject.SetActive(true);
        image.gameObject.SetActive(true);
        text.text = "Flip a card and place it on the area";
        for (int i = 0; i < timeLineTwoObjects.Length; i++)
        {
            timeLineTwoObjects[i].SetActive(true);
        }
    }
    GameObject currentCard = null;
    void CardHasBeenPlaced()
    {

        GameObject[] characters = GameObject.FindGameObjectsWithTag("Cards");
        
        if (currentCard == null) // if card has not been placed
        {
            foreach (GameObject g in characters)
            {
                //The exercise it playing
                // We will then bring up the recording canvas and have the player do pushups and have them click a button when done
                // Then the Other timeline stuff will happen

                if (g.GetComponent<DefaultTrackableEventHandler>().checkIfTracking)
                {
                    currentCard = g;
                    break;
                }
            }
        }
        else if(currentCard != null)
        {
            CardPlacedActivity();

        }
    }
    public bool isWaitingTwo;
    void CardPlacedActivity()
    {
       
        if (currentCard)
        {
            if (!isWaitingTwo)
            {
                text.gameObject.SetActive(true);
                image.gameObject.SetActive(true);
                exerciseComplete.gameObject.SetActive(true);
                // canvasRect.SetActive(true);
                text.text = "Once you are done with the exercise press the buttons";
                GameObject[] temp = new GameObject[3];
                temp[0] = text.gameObject;
                temp[1] = image.gameObject;
                StartCoroutine(TurnOff(10f, temp, isWaitingTwo));
                isWaitingTwo = true;
            }
        }
    }
    public bool isWaiting = false;
    public bool isQuestionTime = false;
    public void HasFinishedExercise()
    {
        isQuestionTime = true;
        text.gameObject.SetActive(true);
        image.gameObject.SetActive(true);
        exerciseComplete.gameObject.SetActive(true);
        if (!isWaiting)
        {
            GameObject[] temp = new GameObject[3];
            temp[0] = text.gameObject;
            temp[1] = image.gameObject;
            temp[2] = exerciseComplete.gameObject;
            StartCoroutine(TurnOff(4f, temp,isWaiting));
            isWaiting = true;
        }
        //canvasRect.SetActive(true);
        text.text = "Now you must answer the questions!";
    }

    IEnumerator Wait()
    {
        isDone = true;
        float time = (float)timeLines[scenario].GetComponent<PlayableDirector>().duration + 2f;
        yield return new WaitForSeconds(time);
        isDone = false;
    }
    IEnumerator TurnOff(float time, GameObject[] temp, bool wait)
    {
        wait = true;
        yield return new WaitForSeconds(time);
        for (int i = 0; i < temp.Length; i++)
        {
            if(temp[i] != null)
                temp[i].SetActive(false);
        }
    }

}
