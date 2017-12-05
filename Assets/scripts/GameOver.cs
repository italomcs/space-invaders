using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text info;

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
