using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnClickChar1 : MonoBehaviour
{
    GameObject SelectedText;
    // Start is called before the first frame update
    void Start()
    {
        SelectedText = GameObject.FindGameObjectWithTag("Selected");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        SelectedText.GetComponent<Text>().text = "Character Selected: 1";
    }
}

