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
    public Text usernameWarning;
    public Text signUpSuccessful;
    private UnityEngine.Random random = new UnityEngine.Random();
    DatabaseReference databaseReference;
    User[] currentUsers;
    public Text text;
    FirebaseApp app;
    DependencyStatus dependencyStatus;
    // Start is called before the first frame update
    void Start()
    {
        dependencyStatus = FirebaseApp.CheckDependencies();
        if (dependencyStatus == Firebase.DependencyStatus.Available)
        {
            // Create and hold a reference to your FirebaseApp,
            // where app is a Firebase.FirebaseApp property of your application class.
            app = Firebase.FirebaseApp.DefaultInstance;
            InitializeFirebase(app);
            // Set a flag here to indicate whether Firebase is ready to use by your app.
        }
        else
        {
            UnityEngine.Debug.LogError(System.String.Format(
              "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
            // Firebase Unity SDK is not safe to use here.
        }
        //Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        //{
        //    dependencyStatus = task.Result;
           
        //});
      //  Debug.Log(dependencyStatus);
       //InitializeFirebase(app);
    }
    void InitializeFirebase(FirebaseApp app)
    {
        // Google
#if UNITY_EDITOR
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://go-health-kids.firebaseio.com/");
        FirebaseAuth.DefaultInstance.App.SetEditorDatabaseUrl("https://go-health-kids.firebaseio.com/");
       // app.SetEditorDatabaseUrl("https://go-health-kids.firebaseio.com/");
#else
        FirebaseApp.DefaultInstance.Options.DatabaseUrl = new System.Uri("https://go-health-kids.firebaseio.com/");
#endif
        //FirebaseApp.DefaultInstance.Options.DatabaseUrl = new System.Uri("https://go-health-kids.firebaseio.com/");
        //Google says:
        //Connects to the database
        // FirebaseAuth.DefaultInstance.
        // app = FirebaseApp.DefaultInstance;
        databaseReference = FirebaseDatabase.DefaultInstance.GetReferenceFromUrl("https://go-health-kids.firebaseio.com/");
        databaseReference.Database.GoOnline();
        GetInformation();

    }
    public void OnSignUp()
    {
        //Grabbing the username and passwords
        string pass = retypePassword.GetComponent<InputField>().text;
        string usernameInfo = userName.text;
        //Making sure the password has not been used before creating the account
        if (pass != "" && password.GetComponentInParent<InputField>().text == pass)
        {
            if (!loginDataBaseHolder.GetComponent<LoginInformation>().CheckForValidUsername(new User(usernameInfo, pass)))
            {
                try
                {
                  
                    //Connecting to the database and sending it the new information
                    if (databaseReference != null)
                        databaseReference.Database.GoOnline();
                    else
                    {
                        databaseReference.Database.GetReferenceFromUrl("https://go-health-kids.firebaseio.com/");
                    }
                    User user = new User(userName.text, retypePassword.GetComponent<InputField>().text);
                    //The unique identifier * Plan to make it an id in the future
                    string key = databaseReference.Child("users").Push().Key;
                    Dictionary<string, object> allUsers = user.ToDictionary();
                    Dictionary<string, object> childUpdates = new Dictionary<string, object>();
                    childUpdates["/Users/" + key] = allUsers;
                    //childUpdates["/user-pass/" + "/" + key] = allUsers;
                    databaseReference.UpdateChildrenAsync(childUpdates);
                    //Recalulate the information
                    //loginDataBaseHolder.GetComponent<LoginInformation>().AddToLoginInformation(user);
                    usernameWarning.GetComponentInParent<InputField>().text = "";
                    signUpSuccessful.GetComponentInParent<InputField>().text = " You have created an account";
                    GetInformation();
                }
                catch
                {
                    //Debug.Log("Couldnt Connect");
                }
            }
            else
            {
                //Debug.Log("Passwords and retype password must match or the username is already taken");
                //Warnings to people depending on if there user name info or password info is wrong
                signUpSuccessful.GetComponentInParent<InputField>().text = "";
                usernameWarning.GetComponentInParent<InputField>().text = "Username is taken";
            }
        }
        else
        {
            signUpSuccessful.GetComponentInParent<InputField>().text = "";
            usernameWarning.GetComponentInParent<InputField>().text = "Passwords must match";
        }

    }
    public void OnSignIn()
    {
        if (loginDataBaseHolder.GetComponent<LoginInformation>().CheckIfLoginIsInDatabase(new User(userName.text, password.GetComponentInParent<InputField>().text)))
        {
            //Move on to the next screen
            databaseReference.Database.GoOffline();
            loginDataBaseHolder.GetComponent<LoginInformation>().AddToLoginInformation(userName.text, password.GetComponentInParent<InputField>().text);
            SceneManager.LoadScene(2);
        }
        else
            usernameWarning.GetComponentInParent<InputField>().text = "Information does not match or it is wrong";
    }
    public void GetInformation()
    {
        //Grabs the information from the database
        FirebaseDatabase.DefaultInstance.GetReference("Users").ValueChanged += LoginAuth_ValueChanged;
    }

    private void LoginAuth_ValueChanged(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }
        //args.Snapshot is where we are able to see the information and grab it
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
            if (!loginDataBaseHolder.GetComponent<LoginInformation>().CheckIfLoginIsInDatabase(temp))
            {
                userInfo.Add(temp);
                loginDataBaseHolder.GetComponent<LoginInformation>().AddToLoginInformation(temp);
            }
            Debug.Log(temp.name + "  " + temp.password);
        }
        return userInfo.ToArray();
    }
    string RemoveNonLettersFromString(string s)
    {
        string newVal = "";
        for (int i = 0; i < s.Length; i++)
        {
            if (Char.IsLetterOrDigit(s[i]))
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

