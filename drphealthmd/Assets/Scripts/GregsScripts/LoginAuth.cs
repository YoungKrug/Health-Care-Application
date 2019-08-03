using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Unity;
using Firebase.Unity.Editor;
using Firebase.Auth;
using Firebase.Database;

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
            string key = databaseReference.Child("scores").Push().Key;
          /*  LeaderBoardEntry entry = new LeaderBoardEntry(userId, score);
            Dictionary<string, Object> allUsers = user.ToDictionary();

            Dictionary<string, Object> childUpdates = new Dictionary<string, Object>();
            childUpdates["/scores/" + key] = entryValues;
            childUpdates["/user-scores/" + userId + "/" + key] = entryValues;

            mDatabase.UpdateChildrenAsync(childUpdates);*/
        }
        catch
        {
            Debug.Log("Couldnt Connect");
        }
        databaseReference.Database.GoOffline();
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
    public Dictionary<string, Object> ToDictionary()
    {
        Dictionary<string, Object> result = new Dictionary<string, Object>();
        //result["name"] = name;
        //result["score"] = score;

        return result;
    }

}

