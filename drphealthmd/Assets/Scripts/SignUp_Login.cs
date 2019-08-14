using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignUp_Login : MonoBehaviour
{
    public GameObject retype;
    public GameObject signUp;
    public GameObject signUpButton;
    public GameObject submitButton;
    public GameObject login;
    public GameObject usernameWarning;
    public GameObject signUpSuccess;
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
        signUpSuccess.GetComponentInParent<InputField>().text = "";
        usernameWarning.GetComponentInParent<InputField>().text = "";
        submitButton.SetActive(false);
        signUpButton.SetActive(true);
        retype.SetActive(true);
        login.SetActive(true);
        signUp.SetActive(false);
    }

    public void BackToLogin()
    {
        signUpSuccess.GetComponentInParent<InputField>().text = "";
        usernameWarning.GetComponentInParent<InputField>().text = "";
        submitButton.SetActive(true);
        signUpButton.SetActive(false);
        retype.SetActive(false);
        login.SetActive(false);
        signUp.SetActive(true);
    }
}
