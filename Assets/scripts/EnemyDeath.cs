using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public Source source;
	public bool death;

	public AudioSource audioSource;

    Texture2D deathImage;
    Enemy enemy;

    int lives;

    void Start ()
	{
        enemy = JsonUtility.FromJson<Settings>(DataManager.data.settings).enemies[int.Parse(transform.parent.parent.name)];
        lives = enemy.lives;
        Damage();
		//transform.parent.GetComponent<Renderer>().material.mainTexture = images[0];
		//Death ();

	}

	void Update ()
	{
		//if (Input.GetKeyDown(KeyCode.Space))
		//{
		//    Death();
		//}

		if(death == true)
		{
			if(audioSource.isPlaying == false)
			{
                GameObject.Find("gamePlay").GetComponent<GamePlayer>().AddScore(enemy.score);
				Destroy(transform.parent.gameObject);
			}
		}
	}

	public void Death()
	{
		transform.parent.GetComponent<Renderer>().material.mainTexture = source.images[enemy.sprite3];
		death = true;
		audioSource.Play ();
	}

    public void Damage()
    {
        lives -= 1;
        if (lives == 0)
        {
            Death();
        }
    }

}