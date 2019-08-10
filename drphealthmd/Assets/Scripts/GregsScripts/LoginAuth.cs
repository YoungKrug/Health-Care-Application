using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Unity;
using Firebase.Unity.Editor;
using Firebase.Auth;
using Firebase.Database;
using Proyecto26;

public class LoginAuth : MonoBehaviour
{
    public Text userName;
    public Text password;
    private Random random = new Random();
    DatabaseReference databaseReference;
    // Start is called before the first frame update
    void Start()
    {
        FirebaseAuth.DefaultInstance.App.SetEditorDatabaseUrl("https://go-health-kids.firebaseio.com/");
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;

    }
    public void OnSubmit()
    {
        try
        {
            databaseReference.Database.GoOnline();
            User user = new User(userName.text, password.text);
          //  string json = JsonUtility.ToJson(user);

           // databaseReference.Child("users").Child("0").SetRawJsonValueAsync(json);
           // RestClient.Put("https://go-health--kids.firebaseio.com/.json", user);
            string key = databaseReference.Child("users").Push().Key;
            //LeaderBoardEntry entry = new LeaderBoardEntry(userId, score);
            Dictionary<string, object> allUsers = user.ToDictionary();

            Dictionary<string, object> childUpdates = new Dictionary<string, object>();
            childUpdates["/Users/" + key] = allUsers;
            childUpdates["/user-password/" + 0 + "/" + key] = allUsers;
            databaseReference.UpdateChildrenAsync(childUpdates);
            GetInformation();
        }
        catch
        {
            Debug.Log("Couldnt Connect");
        }
        databaseReference.Database.GoOffline();
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
        Debug.Log(args.Snapshot);
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
        result["password"] = password;
        return result;
    }
}

