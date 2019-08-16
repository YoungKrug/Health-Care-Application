using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Delay : MonoBehaviour
{
    public GameObject ui;
    public float elapsed = 0f;
    public float time = 3.3f;
    public bool stop = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(stop == false)
        {
            elapsed += Time.deltaTime;
            if(elapsed> time)
            {
                stop = true;
                ui.SetActive(true);
            }
        }
    }
}
