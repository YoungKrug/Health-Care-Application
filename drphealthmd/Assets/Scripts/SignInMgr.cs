using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SignInMgr : MonoBehaviour {
    public GameObject error;
    public InputField Iusername;
    public InputField Ipassword;

    public void checkUser()
    {
        if(Iusername.text.ToString().ToLower() == "guest" && Ipassword.text.ToString().ToLower() == "gohealth")
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            error.SetActive(true);
        }
    }
}
