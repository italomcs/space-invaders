using UnityEngine;
using UnityEngine.UI;

public class GamePlayer : MonoBehaviour
{
    public Text scoreText;
    public Text livesText;

    public int startScore;

    int score;

    float lives;

    Difficulty difficulty;

    void Start ()
    {
        Settings settings = JsonUtility.FromJson<Settings>(DataManager.data.settings);
        difficulty = settings.difficulties[settings.currentDifficulty];

        startScore = settings.startScore;

        scoreText.text = startScore.ToString();

        lives = difficulty.lives;
        livesText.text = lives.ToString();
    }

	void Update ()
    {
        if (lives == 0)
        {
            transform.parent.GetComponent<Hud>().ActivateGameOver("Você morreu");
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
        CheckoutScore(UserProfile.userProfile.email, score);
        transform.parent.GetComponent<Hud>().ActivateWinner("Você eliminou todos os males");
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