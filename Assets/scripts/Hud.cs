using UnityEngine;
using UnityEngine.SceneManagement;

public class Hud : MonoBehaviour
{
    public GameObject gamePlayPanel;
    public GameObject gameOverPanel;
    public GameObject winnerPanel;

    void Start ()
    {
        ActivateGamePlay();
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("start", LoadSceneMode.Single);
        }
    }

    public void ActivateGamePlay()
    {
        gamePlayPanel.SetActive(true);
    }

    public void ActivateGameOver(string infoToShow)
    {
        Destroy(GameObject.Find("player"));
        Destroy(GameObject.Find("enemies"));

        gamePlayPanel.SetActive(false);
        gameOverPanel.SetActive(true);
        gameOverPanel.GetComponent<GameOver>().ShowInfo(infoToShow);
    }

    public void ActivateWinner(string infoToShow)
    {
        Destroy(GameObject.Find("player"));
        Destroy(GameObject.Find("enemies"));

        gamePlayPanel.SetActive(false);
        winnerPanel.SetActive(true);
        winnerPanel.GetComponent<Winner>().ShowInfo(infoToShow);
    }
}
