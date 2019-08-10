using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedIntro : MonoBehaviour
{
    public float Elapsed = 0.0f;
    public float timer = 5.0f;
    public GameObject Char1;
    public GameObject Char2;
    public GameObject Start;
    public GameObject loading;

    // Start is called before the first frame updat
    

    // Update is called once per frame
    void Update()
    {
        this.Elapsed += Time.deltaTime;

        if(Elapsed > timer)
        {
            Appear();
        }
        
    }

    public void Appear()
    {
        Char1.SetActive(true);
        Char2.SetActive(true);
        Start.SetActive(true);
        loading.SetActive(false);
    }
}
