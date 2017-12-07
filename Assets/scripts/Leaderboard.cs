using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Leaderboard : MonoBehaviour
{
    Users users;
    List<User> leaderboardList;

    public Transform list;
    public GameObject userScoreIcon;

    public Text info;

    int numberToshow;
    int page;
    int pages;

    public GameObject emptyInfo;
    public GameObject pageButtons;

    void Start ()
    {
        users = JsonUtility.FromJson<Users>(DataManager.data.users);

        numberToshow = 8;
        page = 0;

        LoadLeaderboard();
        if(leaderboardList.Count != 0)
        {
            emptyInfo.SetActive(false);
            if(leaderboardList.Count > numberToshow)
            {
                pageButtons.SetActive(true);
            }
        }
        ShowLeaderboard(0);
    }

    public void LoadLeaderboard()
    {
        List<int> scores = new List<int>();

        foreach (User user in users.list)
        {
            if (scores.Contains(user.score) == false)
            {
                if(user.score > 0)
                {
                    scores.Add(user.score);
                }
            }
        }

        scores.Sort();
        scores.Reverse();

        leaderboardList = new List<User>();
        foreach (int i in scores)
        {
            foreach (User user in users.list)
            {
                if (user.score == i)
                {
                    leaderboardList.Add(user);
                }
            }
        }

        pages = leaderboardList.Count / numberToshow;
        if (leaderboardList.Count == numberToshow)
        {
            pages = 0;
        }
        float p = leaderboardList.Count / (float)numberToshow;
        if (p - (leaderboardList.Count / numberToshow) != 0)
        {
            pages += 1;
        }

        info.text = (page + 1) + "/" + pages;

    }

    public void ShowLeaderboard(int page)
    {
        foreach (Transform child in list)
        {
            Destroy(child.gameObject);
        }

        int index = 0;
        foreach (User user in leaderboardList)
        {
            if (index >= (page * numberToshow) && index < ((page * numberToshow) + numberToshow))
            {
                GameObject addUserScoreIcon = Instantiate(userScoreIcon, list);
                Text t = addUserScoreIcon.transform.Find("Panel/Text").GetComponent<Text>();
                t.text = user.name;
                Text tt = addUserScoreIcon.transform.Find("Panel (1)/Text").GetComponent<Text>();
                tt.text = user.score.ToString();

                if (user.email == UserProfile.userProfile.email)
                {
                    Color c = Color.green;
                    t.color = c;
                    tt.color = c;
                }
            }
            index += 1;
        }

        info.text = (page + 1) + "/" + pages;
    }

    public void NextPage()
    {
        if (page + 1 < pages)
        {
            page += 1;
        }
        ShowLeaderboard(page);
    }
    public void BackPage()
    {
        if (page > 0)
        {
            page -= 1;
            ShowLeaderboard(page);
        }
    }

    public void Done()
    {
        SceneManager.LoadScene("start", LoadSceneMode.Single);
    }
}
