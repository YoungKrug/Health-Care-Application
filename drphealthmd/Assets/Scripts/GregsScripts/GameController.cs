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
    public GameObject introStuff;
    public Text introText;
    [HideInInspector]
    public List<GameObject> playArea;
    public List<GameObject> timeLines;
    public List<GameObject> timeLineOneObjects;
    public List<GameObject> timeLineTwoObjects;
    public List<GameObject> timeLineThreeObjects; // Code for a way to make this all work after a certain prereq has be fullfilled
    public List<GameObject> timeLineFourObjects;
    public List<GameObject> timeLineFiveObjects;
    public GameObject recordCanvas;
    public GameObject placer;
    public GameObject planeFinder;
    public GameObject objectToSpawnOnCard;
    public GameObject cardArea;
    public GameObject canvasRect;
    public Image image;
    public Button button;
    public Button exerciseComplete;
    public Text text;
    public GameObject playerOneObj;
    public GameObject playerTwoObj;
    public Text debug;
    public bool isWaitingForPlayerInput = true;
    public GameObject characterSelectionCanvas;
    public GameObject characters;
    int numberOfTouches = 0;
    int scenario = 0;
    bool isDoingFirstScenario = false;
    bool cardHasBeenPlaced;
    bool isDone;
    bool hasTriggered = false;
    bool isWaitingForCourotine;
    bool canContinue = false;
    // Since we are not doing the card flip we need to change up how we say it
    void Awake()
    {
        image.gameObject.SetActive(false);
        text.gameObject.SetActive(false);
        button.gameObject.SetActive(false);
    }
    private void Start()
    {
        for (int i = 0; i < timeLineTwoObjects.Count; i++)
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
            if(scenario == 1)
            {
                characterSelectionCanvas.SetActive(true);
                characters.SetActive(true);
            }
            isDoingFirstScenario = true;
            if (scenario == 3 && !isQuestionTime && !hasTriggered) // continue
            {
                HandleScenarioInformation();
                hasTriggered = true;
            }
            if (scenario != 3 && !hasTriggered)
            {
               if(scenario == 1 && canContinue)
               {
                    HandleScenarioInformation();
                    characterSelectionCanvas.SetActive(false);
                    characters.SetActive(false);
                }
               else if(scenario != 1)
                    HandleScenarioInformation();
            }
        }
        //We know the player has given input, but we have to ensure he wants to use this location
        // int touches = 0;      

        if (isWaitingForPlayerInput && numberOfTouches > 0 && placer.GetComponent<DefaultTrackableEventHandler>().checkIfTracking)
            CheckIfObjectHasBeenPlaced();
        DebugText();
        numberOfTouches += Input.touches.Length;
        numberOfTouches += Input.touchCount;
    }
    //Checks if the player has placed the object
    void HandleScenarioInformation()
    {
        if (scenario == 0)
        {
            DontMove[] objs = GameObject.FindObjectsOfType(typeof(DontMove)) as DontMove[];
            for (int i = 0; i < objs.Length; i++)
            {
                playArea.Add(objs[i].gameObject);
            }
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
            introText.gameObject.SetActive(false);
            introStuff.SetActive(false);
        }
    }
    public void CanContinue()
    {
        canContinue = true;
        characterSelectionCanvas.SetActive(false);
      //  scenario++;
       // HandleScenarioInformation();
    }
    //When someone clicks the button we have the player input
    public void SetIsWaitingForPlayerInput()
    {
        isWaitingForPlayerInput = false;
    }
    void PlayScenario()
    {
        //Plays the timeline squences
        if (scenario < 1)
        {
            SetListEqualToTrue(timeLineOneObjects);
            StartCoroutine(Wait());
            timeLines[scenario].SetActive(true);
            //introStuff.SetActive(false);
        }
        if (scenario == 1)
        {
            SetListEqualToFalse(timeLineOneObjects);
            SetListEqualToTrue(timeLineTwoObjects);
            StartCoroutine(Wait());
            timeLines[scenario - 1].SetActive(true);
            timeLines[scenario].SetActive(true);
            //timeLines[scenario].SetActive(true);
            // return;
        }
        if (scenario == 2)
        {
            SetListEqualToFalse(timeLineTwoObjects);
            SetListEqualToTrue(timeLineThreeObjects);
            StartCoroutine(Wait());
            timeLines[scenario].SetActive(true);            

            //timeLines[scenario].SetActive(true);
            // return;
        }
        if (scenario == 3)
        {
            playerOneObj.SetActive(true);
            recordCanvas.SetActive(true);
            //timeLines[scenario].SetActive(true);
            // return;
        }

        scenario++;
    }
    void SetListEqualToTrue(List<GameObject> temp)
    {
        foreach (GameObject g in temp)
        {
            g.SetActive(true);
        }
    }
    void SetListEqualToFalse(List<GameObject> temp)
    {
        foreach (GameObject g in temp)
        {
            g.SetActive(false);
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
        for (int i = 0; i < timeLineTwoObjects.Count; i++)
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
        else if (currentCard != null)
        {
            CardPlacedActivity();

        }
    }
    public bool isWaitingTwo;
    public void DoesPlayerTwoHaveThePhone()
    {
        playerOneObj.SetActive(false);
        CardPlacedActivity();
    }
    void CardPlacedActivity()
    {
        text.gameObject.SetActive(true);
        image.gameObject.SetActive(true);
        exerciseComplete.gameObject.SetActive(true);
        // canvasRect.SetActive(true);
        text.text = "Once you are done with the exercise press the buttons";
        GameObject[] temp = new GameObject[3];
        temp[0] = text.gameObject;
        temp[1] = image.gameObject;
        StartCoroutine(TurnOff(20f, temp, isWaitingTwo));
        isWaitingTwo = true;
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
            StartCoroutine(TurnOff(4f, temp, isWaiting));
            isWaiting = true;
        }
        //canvasRect.SetActive(true);
        text.text = "Done";
        //For reference
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
            if (temp[i] != null)
                temp[i].SetActive(false);
        }
    }

}
