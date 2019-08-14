using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignUp_Login : MonoBehaviour
{
    public GameObject retype;
    public GameObject signUp;
    public GameObject signUpButton;
    public GameObject submitButton;
    public GameObject login;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
  
    }

    public void Toggle()
    {
        submitButton.SetActive(false);
        signUpButton.SetActive(true);
        retype.SetActive(true);
        login.SetActive(true);
        signUp.SetActive(false);
    }

    public void BackToLogin()
    {
        submitButton.SetActive(true);
        signUpButton.SetActive(false);
        retype.SetActive(false);
        login.SetActive(false);
        signUp.SetActive(true);
    }
}
