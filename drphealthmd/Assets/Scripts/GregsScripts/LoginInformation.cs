using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginInformation : MonoBehaviour
{
    List<User> usersInformation = new List<User>();
    // Start is called before the first frame update
    // Update is called once per frame
    public void AddToLoginInformation(User u)
    {
        usersInformation.Add(u);
    }
    // Function overload
    public void AddToLoginInformation(string name, string password)
    {
        usersInformation.Add(new User(name,password));
    }
    public bool CheckIfLoginIsInDatabase(User u)
    {
        for (int i = 0; i < usersInformation.Count; i++)
        {
            if(u == usersInformation[i])
            {
                return true;
            }
        }
        return false;
    }
    public bool CheckForValidUsername(User u)
    {
        for (int i = 0; i < usersInformation.Count; i++)
        {
            if (u.name == usersInformation[i].name)
            {
                return true;
            }
        }
        return false;
    }
    public bool CheckForValidUsername(string u)
    {
        for (int i = 0; i < usersInformation.Count; i++)
        {
            if (u == usersInformation[i].name)
            {
                return true;
            }
        }
        return false;
    }
}
