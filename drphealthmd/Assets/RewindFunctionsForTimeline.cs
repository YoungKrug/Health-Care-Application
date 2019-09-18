using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class RewindFunctionsForTimeline : MonoBehaviour
{
    public PlayableDirector[] playables;
    public int currentPlayable;
    public float speed;
    bool rewind;
    bool paused;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(rewind)
        {
            StartRewind();
        }
    }
    void StartRewind()
    {
        paused = false;
        Time.timeScale = 1f;
        Debug.Log(currentPlayable);
        PlayableDirector temp = playables[currentPlayable];
        float timeToRewind = (Time.deltaTime * speed);

        if (temp.time - timeToRewind >= 0.1f)
        {
            temp.time -= timeToRewind;
            Debug.Log(temp.time);
        }
        else
            Play();
        temp.Evaluate();
    }
    public void GetTimelineInfo(int directorNum)
    {
        currentPlayable = directorNum;
    }
    public void Rewind()
    {
        rewind = true;      
    }
    public void Play()
    {
        if (rewind || paused)
        {
            Time.timeScale = 1f;
            rewind = false;
            paused = false;
            PlayableDirector temp = playables[currentPlayable];
            temp.Evaluate();
            //temp.Play(temp.playableAsset);
        }
    }
    public void Pause()
    {
        //PlayableDirector temp = playables[currentPlayable];
        //temp.Stop();
        Time.timeScale = 0.0000001f;
        paused = true;
        rewind = false;
    }
}
