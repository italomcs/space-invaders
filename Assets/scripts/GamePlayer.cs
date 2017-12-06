using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamePlayer : MonoBehaviour
{
    public Text scoreText;
    public Text livesText;

    public int startScore;

    public static int score;

    static float lives;

    Difficulty difficulty;

    public static bool initialized;

    void Start ()
    {
        Settings settings = JsonUtility.FromJson<Settings>(DataManager.data.settings);
        difficulty = settings.difficulties[settings.currentDifficulty];

        //startScore = settings.startScore;

        scoreText.text = score.ToString();

        if (initialized == false)
        {
            lives = difficulty.lives;
        }
        livesText.text = lives.ToString();

        initialized = true;
    }

	void Update ()
    {
        if (lives == 0)
        {
            //transform.parent.GetComponent<Hud>().ActivateGameOver("Você morreu");
            transform.parent.GetComponent<Hud>().ActivateGameOver("Score:"+score.ToString());
        }
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = score.ToString();
    }

    public void SubLives()
    {
        lives -= 1;
        livesText.text = lives.ToString();
    }

    public void Winner()
    {
        //CheckoutScore(UserProfile.userProfile.email, score);
        //transform.parent.GetComponent<Hud>().ActivateWinner("Você eliminou todos os males");
        SceneManager.LoadScene("gameplay", LoadSceneMode.Single);
    }

    public void GameOver()
    {
        transform.parent.GetComponent<Hud>().ActivateGameOver("Você morreu");
    }

    public void CheckoutScore(string email, int score)
    {
        Users users = JsonUtility.FromJson<Users>(DataManager.data.users);
        foreach (User user in users.list)
        {
            if (user.email == email)
            {
                if(score > user.score)
                {
                    user.score = score;
                }
            }
        }
        DataManager.data.users = JsonUtility.ToJson(users);
    }
}