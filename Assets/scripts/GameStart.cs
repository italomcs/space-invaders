using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("login", LoadSceneMode.Single);
    }

    public void Ranking()
    {
        SceneManager.LoadScene("leaderboard", LoadSceneMode.Single);
    }

    public void Quit()
    {
        Application.Quit();
    }
}