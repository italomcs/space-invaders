using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text info;

    public DataManager dataManager;

    private void Start()
    {
        dataManager.SaveData();
    }

    void Update ()
    {
		if(Input.anyKeyDown)
        {
            //SceneManager.LoadScene("start", LoadSceneMode.Single);
            SceneManager.LoadScene("leaderboard", LoadSceneMode.Single);
        }
	}

    public void ShowInfo(string infoToShow)
    {
        info.text = infoToShow;
    }
}
