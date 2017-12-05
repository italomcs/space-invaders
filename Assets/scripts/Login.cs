using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    Users users;

    public InputField userName;
    public InputField userEmail;

    public DataManager dataManager;

    bool done;

    void Start ()
    {
        users = JsonUtility.FromJson<Users>(DataManager.data.users);

        UserProfile.userProfile = new User();
    }

    public void Done()
    {
        if(!string.IsNullOrEmpty(userName.text) && !string.IsNullOrEmpty(userEmail.text))
        {
            if(done == false)
            {
                AddUser(userName.text, userEmail.text);
                dataManager.SaveData();
                Invoke("LoadIntroScene", 1);
                done = true;
            }
        }
    }

    public void AddUser(string newName, string newEmail)
    {
        User newUser = new User();
        foreach (User u in users.list)
        {
            if (u.email == newEmail)
            {
                newUser = u;
            }
        }

        if (newUser.email == null)
        {
            newUser.name = newName;
            newUser.email = newEmail;

            users.list.Add(newUser);
        }
        else
        {
            foreach (User u in users.list)
            {
                if (u.email == newEmail)
                {
                    u.name = newName;
                }
            }
        }

        DataManager.data.users = JsonUtility.ToJson(users);

        UserProfile.userProfile.name = newUser.name;
        UserProfile.userProfile.email = newUser.email;
    }

    public void LoadIntroScene()
    {
        SceneManager.LoadScene("gameplay", LoadSceneMode.Single);
    }

    public void Cancel()
    {
        //SceneManager.LoadScene("start", LoadSceneMode.Single);
        Application.Quit();
    }
}