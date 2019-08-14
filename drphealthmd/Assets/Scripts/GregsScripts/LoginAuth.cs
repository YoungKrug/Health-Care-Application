using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Unity;
using Firebase.Unity.Editor;
using Firebase.Auth;
using Firebase.Database;
using System;
using UnityEngine.SceneManagement;

public class LoginAuth : MonoBehaviour
{
    public GameObject retypePassword;
    public GameObject loginDataBaseHolder;
    public Text userName;
    public Text password;
    public GameObject passwordInputField;
    private UnityEngine.Random random = new UnityEngine.Random();
    DatabaseReference databaseReference;
    User[] currentUsers;
    // Start is called before the first frame update
    void Start()
    {
        FirebaseAuth.DefaultInstance.App.SetEditorDatabaseUrl("https://go-health-kids.firebaseio.com/");
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        GetInformation();

    }
    public void OnSignUp()
    {
        if (retypePassword.GetComponent<InputField>().text != "" && password.GetComponentInParent<InputField>().text == retypePassword.GetComponent<InputField>().text)
        {
            try
            {
                databaseReference.Database.GoOnline();
                User user = new User(userName.text, retypePassword.GetComponent<InputField>().text);
                string key = databaseReference.Child("users").Push().Key;
                Dictionary<string, object> allUsers = user.ToDictionary();
                Dictionary<string, object> childUpdates = new Dictionary<string, object>();
                childUpdates["/Users/" + key] = allUsers;
                //childUpdates["/user-pass/" + "/" + key] = allUsers;
                databaseReference.UpdateChildrenAsync(childUpdates);
            }
            catch
            {
                Debug.Log("Couldnt Connect");
            }
        }
        else
        {
            //Send message
            Debug.Log("Passwords and retype password must match");
        }
    }
    public void OnSignIn()
    {
        if (loginDataBaseHolder.GetComponent<LoginInformation>().CheckIfLoginIsInDatabase(new User(userName.text, password.GetComponentInParent<InputField>().text)))
        {
            //Move on to the next screen
            databaseReference.Database.GoOffline();
            loginDataBaseHolder.GetComponent<LoginInformation>().AddToLoginInformation(userName.text, password.GetComponentInParent<InputField>().text);
            SceneManager.LoadScene(1);
        }
    }
    public void GetInformation()
    {
        FirebaseDatabase.DefaultInstance.GetReference("Users").ValueChanged += LoginAuth_ValueChanged;
    }

    private void LoginAuth_ValueChanged(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }
        //string key = args.Snapshot.Key;
        string info = args.Snapshot.GetRawJsonValue();
        string info2 = args.Snapshot.GetRawJsonValue();
        int count = (int)args.Snapshot.ChildrenCount;
       currentUsers = GetInformationFromJson(info, info2, count);
      //  Debug.Log(args.Snapshot.Value);
      //  Debug.Log(args.Snapshot);
    }
    User[] GetInformationFromJson(string s, string b, int length)
    {
        //Passwordstring is a key that i will use to find where the password is
        List<User> userInfo = new List<User>();
        int incrememnt = 0;
        string passwordString = "passwordGenerateLengthToBeLongerSoPeopleCannotCopyIt";
        //int incrememntTwo = 1;
        for (int i = 0; i < length; i++)
        {
            string valueName = s.Substring(s.IndexOf("name") + "name".Length + incrememnt);
            s = s.Substring(s.IndexOf("name") + "name".Length + incrememnt);
            string valuePassword = b.Substring(b.IndexOf(passwordString) + passwordString.Length + incrememnt);
            b = b.Substring(b.IndexOf(passwordString) + passwordString.Length + incrememnt);
            if (valueName.Contains(","))
            {
                //Debug.Log(valueTwo.IndexOf(","));
                valueName = valueName.Remove(valueName.IndexOf(","));
                
                if(i == length-1)
                    valuePassword = valuePassword.Remove(valuePassword.IndexOf("}"));
                else
                    valuePassword = valuePassword.Remove(valuePassword.IndexOf(","));
            }
            valueName = RemoveNonLettersFromString(valueName);
            valuePassword = RemoveNonLettersFromString(valuePassword);
            // Going to create a information Handler for the information
            User temp = new User(valueName, valuePassword);
            userInfo.Add(temp);
            loginDataBaseHolder.GetComponent<LoginInformation>().AddToLoginInformation(temp);
            Debug.Log(temp.name + "  " + temp.password);
        }
        return userInfo.ToArray();
    }
    string RemoveNonLettersFromString(string s)
    {
        string newVal = "";
        for (int i = 0; i < s.Length; i++)
        {
            if (Char.IsLetter(s[i]))
            {
                newVal += s[i];
            }
        }
        return newVal;
    }
}
public class User
{
    public User(string n, string p)
    {
        name = n;
        password = p;
    }
    public string name;
    public string password;
    public Dictionary<string, object> ToDictionary()
    {
        Dictionary<string, object> result = new Dictionary<string, object>();
        result["name"] = name;
        result["passwordGenerateLengthToBeLongerSoPeopleCannotCopyIt"] = password;
        return result;
    }
    public static bool operator==(User user1, User user2)
    {
        return (user1.password == user2.password) && (user1.name == user2.name);
    }
    public static bool operator!=(User user1, User user2)
    {
        return (user1.password != user2.password) && (user1.name != user2.name);
    }
}

