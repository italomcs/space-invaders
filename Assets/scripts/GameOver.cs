using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text info;

    private void Start()
    {
        GamePlayer.score = 0;
        GamePlayer.initialized = false;
        Enemies.fireFrequency = 0;
        Enemies.refreshSpeedLevel = 0;
    }

    void Update ()
    {
		if(Input.anyKeyDown)
        {
            //SceneManager.LoadScene("start", LoadSceneMode.Single);
            SceneManager.LoadScene("login", LoadSceneMode.Single);
        }
	}

    public void ShowInfo(string infoToShow)
    {
        info.text = infoToShow;
    }
}
