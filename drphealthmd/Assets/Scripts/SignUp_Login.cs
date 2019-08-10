using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignUp_Login : MonoBehaviour
{
    public GameObject retype;
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

    public void Toggle()
    {
        retype.SetActive(true);
        login.SetActive(true);
        signUp.SetActive(false);
    }

    public void BackToLogin()
    {
        retype.SetActive(false);
        login.SetActive(false);
        signUp.SetActive(true);
    }
}
