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
        GetInformation();

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
        //string key = args.Snapshot.Key;
        string info = args.Snapshot.GetRawJsonValue();
        string info2 = args.Snapshot.GetRawJsonValue();
        int count = (int)args.Snapshot.ChildrenCount;
        User[] allUsers = GetInformationFromJson(info, info2, count);
      //  Debug.Log(args.Snapshot.Value);
      //  Debug.Log(args.Snapshot);
    }
    User[] GetInformationFromJson(string s, string b, int length)
    {
        List<User> userInfo = new List<User>();
        int incrememnt = 0;
        //int incrememntTwo = 1;
        for (int i = 0; i < length; i++)
        {
            string value = s.Substring(s.IndexOf("name") + "name".Length + incrememnt);
            s = s.Substring(s.IndexOf("name") + "name".Length + incrememnt);
            string valueTwo = b.Substring(b.IndexOf("password") + "password".Length + incrememnt);
            b = b.Substring(b.IndexOf("password") + "password".Length + incrememnt);
            if (value.Contains(","))
            {
                //Debug.Log(valueTwo.IndexOf(","));
                value = value.Remove(value.IndexOf(","));
                if(i == length-1)
                    valueTwo = valueTwo.Remove(valueTwo.IndexOf("}"));
                else
                    valueTwo = valueTwo.Remove(valueTwo.IndexOf(","));
            }
            
            Debug.Log(value + " : " + valueTwo);
            string userName = "";
            string password = "";
            User temp = new User(userName, password);                                                         
            //incrememnt += 
           // incrememnt += (s.IndexOf("name") + "name".Length + incrememnt);
           // incrememntTwo += (b.IndexOf("name") + "name".Length + incrememntTwo);
            
        }
        return userInfo.ToArray();
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

