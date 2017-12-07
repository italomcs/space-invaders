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

	bool keyboardIsEnable;

	VirtualKeyboard vk = new VirtualKeyboard();

    void Start ()
    {
        users = JsonUtility.FromJson<Users>(DataManager.data.users);

        UserProfile.userProfile = new User();
        GamePlayer.score = 0;
        GamePlayer.initialized = false;
        Enemies.fireFrequency = 0;
        Enemies.refreshSpeedLevel = 0;
    }

	void Update()
	{
		if (userName.isFocused || userEmail.isFocused)
		{
			if (keyboardIsEnable == false && Input.GetKeyUp(KeyCode.Mouse0))
			{
				OpenKeyboard ();
				keyboardIsEnable = true;
			}
		}
		else if(keyboardIsEnable == true)
		{
			CloseKeyboard ();
			keyboardIsEnable = false;
		}
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

	public void OpenKeyboard()
	{
		{       
			vk.ShowTouchKeyboard();
		}
	}

	public void CloseKeyboard()
	{
		{       
			vk.HideTouchKeyboard();
		}
	}
}