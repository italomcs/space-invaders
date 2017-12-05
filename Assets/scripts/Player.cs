using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    DataManager dataManager;

    PlayerData player;

    public Source source;

    public GameObject bullet;

    float fireSpeed;
    float fireTime;

    public Transform addBulletPoint;

    public AudioSource audioSource;

    public bool pin;

    public bool death;

    Vector3 startPositition;

    void Start ()
    {
        /*
        if (GameObject.Find("dataManager") == null)
        {
            dataManager = new GameObject("dataManager").AddComponent<DataManager>();
        }
        else
        {
            dataManager = GameObject.Find("dataManager").GetComponent<DataManager>();
        }

        if (string.IsNullOrEmpty(DataManager.data.settings))
        {
            DataManager.data.settings = "{\"lives\":3,\"sprite\":8,\"sprite1\":6,\"bulletSprite\":6,\"fireSpeed\":0.5,\"bulletSpeed\":5}";
        }
        */

        player = JsonUtility.FromJson<Settings>(DataManager.data.settings).player;

        fireSpeed = player.fireSpeed;

        GetComponent<Renderer>().material.mainTexture = source.images[player.sprite];

        Vector3 v = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, 0, 0));
        startPositition = new Vector3(v.x, transform.position.y, v.z + transform.localScale.x / 2);

        transform.position = startPositition;
    }

	void Update ()
    {
        if(death == false)
        {
            fireTime += Time.deltaTime*fireSpeed;
            if (fireTime >= 1)
            {
                GameObject newBullet = Instantiate(bullet, addBulletPoint.position, addBulletPoint.rotation);
                newBullet.transform.tag = "bullet";
                newBullet.GetComponent<Bullet>().bulletSpeed = player.bulletSpeed;
                newBullet.GetComponent<Renderer>().material.mainTexture = source.images[player.bulletSprite];
                Destroy(newBullet, 5);
                audioSource.Play();
                fireTime = 0;
            }

            if (Input.GetKey(KeyCode.Mouse0))
            {
                Vector3 screeToWord = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                if (Vector3.Distance(screeToWord, transform.position) <= 1.5f && pin == false)
                {
                    pin = true;
                }

                if (pin == true)
                {
                    transform.position = new Vector3(screeToWord.x, transform.position.y, transform.position.z);
                }
            }

            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                pin = false;
            }

            foreach (GameObject bullet in GameObject.FindGameObjectsWithTag("enemyBullet"))
            {
                if (Vector3.Distance(bullet.transform.position, transform.position) <= 0.5f)
                {
                    Death();
                }
            }
        }
        else
        {
            if (audioSource.isPlaying == false)
            {
                GameObject.Find("gamePlay").GetComponent<GamePlayer>().SubLives();
                GetComponent<Renderer>().material.mainTexture = source.images[player.sprite];
                audioSource.clip = source.sounds[1];
                transform.position = startPositition;
                pin = false;
                death = false;
            }
        }
	}

    public void Death()
    {
        GetComponent<Renderer>().material.mainTexture = source.images[player.sprite1];
        death = true;
        audioSource.clip = source.sounds[0];
        audioSource.Play();
    }
}