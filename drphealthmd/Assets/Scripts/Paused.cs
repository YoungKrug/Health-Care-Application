using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paused : MonoBehaviour
{
    public GameObject paused;
    public bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void IsPaused()
    {
        if(!isPaused)
        {
            paused.SetActive(true);
            isPaused = true;
            Time.timeScale = 0;
        }
        else
        {
            paused.SetActive(false);
            isPaused = false;
            Time.timeScale = 1;
        }

    }


}
