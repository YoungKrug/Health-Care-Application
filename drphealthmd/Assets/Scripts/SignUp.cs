using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignUp : MonoBehaviour
{
    public GameObject InputField;
    public GameObject signUp;
    public GameObject login;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Retype()
    {

        InputField.SetActive(true);
        signUp.SetActive(false);
        login.SetActive(true);
    }
    public void Login()
    {
        signUp.SetActive(true);
        login.SetActive(false);
        InputField.SetActive(false);
    }
}


