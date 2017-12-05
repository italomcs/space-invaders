using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Winner : MonoBehaviour
{
    public Text info;
    public DataManager dataManager;
    bool done;

    private void Start()
    {
        dataManager.SaveData();
    }

    void Update()
    {
        if (Input.anyKeyDown && done == false)
        {
            Invoke("LoadLeaderboardScene", 1);
            done = true;
        }
    }

    public void ShowInfo(string infoToShow)
    {
        info.text = infoToShow;
    }

    public void LoadLeaderboardScene()
    {
        //SceneManager.LoadScene("leaderboard", LoadSceneMode.Single);
        SceneManager.LoadScene("login", LoadSceneMode.Single);
    }
}