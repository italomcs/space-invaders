using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemies : MonoBehaviour
{
    DataManager dataManager;

    public Source source;
    public GameObject enemy;

    //difficulty
    public float refreshMovement;
    public float refreshSpeed;
    public float decrasePercent;
    public float fireFrequency;
    public float bulletSpeed;

    float currentRefreshTime;

    float direction;

    int levelHelp;

    public AudioSource audioSource;
    public AudioClip[] sounds;
    int currentSound;

    int spritesHelper;

    List<Enemy> enemiesData;

    public GameObject enemyDeath;

    public GameObject bullet;

    public GameObject playerAudio;

    public Transform player;

    bool gameover;

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
        */

        //if (string.IsNullOrEmpty(DataManager.data.settings))
        //{
        //    DataManager.data.settings = "{\"player\":{\"sprite\":8,\"sprite1\":6,\"bulletSprite\":6,\"fireSpeed\":0.5,\"bulletSpeed\":5},\"enemies\":[{\"title\":\"Azul\",\"lives\":1,\"score\":10,\"sprite1\":0,\"sprite2\":1,\"sprite3\":6,\"sprite4\":7,\"deathSound\":0},{\"title\":\"Verde\",\"lives\":2,\"score\":20,\"sprite1\":2,\"sprite2\":3,\"sprite3\":6,\"sprite4\":7,\"deathSound\":0},{\"title\":\"Vermelho\",\"lives\":3,\"score\":30,\"sprite1\":4,\"sprite2\":5,\"sprite3\":6,\"sprite4\":7,\"deathSound\":0}],\"difficulties\":[{\"title\":\"Facil\",\"lives\":3,\"refreshMovement\":0.2,\"refreshSpeed\":1,\"decrasePercent\":0.25,\"fireFrequency\":3,\"bulletSpeed\":5},{\"title\":\"Medio\",\"lives\":2,\"refreshMovement\":0.2,\"refreshSpeed\":0.8,\"decrasePercent\":0.25,\"fireFrequency\":2,\"bulletSpeed\":5},{\"title\":\"Dificil\",\"lives\":1,\"refreshMovement\":0.2,\"refreshSpeed\":0.6,\"decrasePercent\":0.25,\"fireFrequency\":1,\"bulletSpeed\":5},{\"title\":\"Personalizada\",\"lives\":10,\"refreshMovement\":0.2,\"refreshSpeed\":0.6,\"decrasePercent\":0.25,\"fireFrequency\":1,\"bulletSpeed\":5}],\"currentDifficulty\": 1}";
        //}
        


        Settings settings = JsonUtility.FromJson<Settings>(DataManager.data.settings);

        Difficulty difficulty = settings.difficulties[settings.currentDifficulty];
        refreshMovement = difficulty.refreshMovement;
        refreshSpeed = difficulty.refreshSpeed;
        decrasePercent = difficulty.decrasePercent;
        fireFrequency = difficulty.fireFrequency;
        bulletSpeed = difficulty.bulletSpeed;

        enemiesData = settings.enemies;

        direction = refreshMovement;

        LoadEnemies();

        InvokeRepeating("Fire", fireFrequency, fireFrequency);
    }

    private void Update()
    {
        /*
        if(Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
            if(enemies.Length > 0)
            {
                Destroy(enemies[enemies.Length - 1]);
            }
        }
        */

        currentRefreshTime += Time.deltaTime*refreshSpeed;
        if (currentRefreshTime >= 1)
        {
            Refresh();
            currentRefreshTime = 0;
        }

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("enemy"))
        {
            if (enemy.transform.parent.parent == transform)
            {
                foreach (GameObject bullet in GameObject.FindGameObjectsWithTag("bullet"))
                {
                    if (Vector3.Distance(bullet.transform.position, enemy.transform.position) <= 0.5f)
                    {
                        if(enemy.transform.Find("enemyDeath(Clone)") == null)
                        {
                            Instantiate(enemyDeath, enemy.transform);
                            
                        }
                        else
                        {
                            enemy.transform.Find("enemyDeath(Clone)").GetComponent<EnemyDeath>().Damage();
                        }
                        Destroy(bullet);
                    }
                }
            }
        }

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Fire();
        //}
    }

    void LoadEnemies()
    {
        foreach (Transform child in transform)
        {
            int childIndex = int.Parse(child.name);

            for (int i = 0; i < 10; i++)
            {
                GameObject addEnemy = Instantiate(enemy, child);
                addEnemy.transform.position = addEnemy.transform.position + new Vector3(i*1.5f, 0, 0);
                addEnemy.GetComponent<Renderer>().material.mainTexture = source.images[enemiesData[childIndex].sprite1];
                GameObject textobj = addEnemy.transform.Find("New Text").gameObject;
                textobj.GetComponent<TextMesh>().text = enemiesData[childIndex].title;
                textobj.AddComponent<BoxCollider>();
                float scale = 1f / textobj.GetComponent<BoxCollider>().size.x;
                if (scale < textobj.transform.localScale.x)
                {
                    textobj.transform.localScale = new Vector3(scale, scale, scale);
                }
            }
        }
    }

    void Refresh()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        if (enemies.Length > 0)
        {
            Array.Sort(enemies, DistanceSort);
            float distance = Vector3.Distance(transform.position, enemies[enemies.Length - 1].transform.position);
            float distance1 = Vector3.Distance(transform.position, enemies[0].transform.position);

            Vector3 screenLeftPoint = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
            Vector3 screenRightPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));

            float distanceLeft = Vector3.Distance(new Vector3(transform.position.x + distance1, transform.position.y, transform.position.z), new Vector3(screenLeftPoint.x, transform.position.y, transform.position.z));
            float distanceRight = Vector3.Distance(new Vector3(transform.position.x + distance, transform.position.y, transform.position.z), new Vector3(screenRightPoint.x, transform.position.y, transform.position.z));

            if (distanceLeft > 1 && distanceRight > 1)
            {
                transform.Translate(direction, 0, 0);
            }
            else
            {
                if (distanceRight < 1)
                {
                    if (direction > 0)
                    {
                        transform.Translate(0, 0, -0.5f);
                        direction = -refreshMovement;
                        currentSound = 0;
                        audioSource.pitch += 0.1f;
                        //refreshTime -= (refreshTime * 0.25f);
                    }
                    else
                    {
                        transform.Translate(direction, 0, 0);
                    }
                }
                else if (distanceLeft < 1)
                {
                    if (direction < 0)
                    {
                        transform.Translate(0, 0, -0.5f);
                        direction = refreshMovement;
                        currentSound = 0;
                        audioSource.pitch += 0.1f;
                        //refreshTime -= (refreshTime * 0.25f);
                    }
                    else
                    {
                        transform.Translate(direction, 0, 0);
                    }
                }
            }

            int length = enemies.Length;
            if (length > 25 && length <= 30)
            {
                //refreshTime -= (refreshTime * 0.25f);
                //refreshSpeed = 1f;
            }
            else if (length > 20 && length <= 25 && levelHelp == 0)
            {
                //refreshTime = 0.84f;
                refreshSpeed += (refreshSpeed * decrasePercent);
                levelHelp = 1;
            }
            else if (length > 15 && length <= 20f && levelHelp == 1)
            {
                //refreshTime = 0.68f;
                refreshSpeed += (refreshSpeed * decrasePercent);
                levelHelp = 2;
            }
            else if (length > 10 && length <= 15f && levelHelp == 2)
            {
                //refreshTime = 0.52f;
                refreshSpeed += (refreshSpeed * decrasePercent);
                levelHelp = 3;
            }
            else if (length > 5 && length <= 10f && levelHelp == 3)
            {
                //refreshTime = 0.36f;
                refreshSpeed += (refreshSpeed * decrasePercent);
                levelHelp = 4;
            }
            else if (length == 5f && levelHelp == 4)
            {
                //refreshTime = 0.20f;
                refreshSpeed += (refreshSpeed * decrasePercent);
                levelHelp = 5;
            }
            else if (length == 4f && levelHelp == 5)
            {
                //refreshTime = 0.15f;
                refreshSpeed += (refreshSpeed * decrasePercent);
                levelHelp = 6;
            }
            else if (length == 3f && levelHelp == 6)
            {
                //refreshTime = 0.10f;
                refreshSpeed += (refreshSpeed * decrasePercent);
                levelHelp = 7;

            }
            else if (length == 2f && levelHelp == 7)
            {
                //refreshTime = 0.075f;
                refreshSpeed += (refreshSpeed * decrasePercent);
                levelHelp = 8;
            }
            else if (length == 1f && levelHelp == 8)
            {
                //refreshTime = 0.05f;
                refreshSpeed += (refreshSpeed * decrasePercent);
                levelHelp = 9;
            }

            foreach (Transform child in transform)
            {
                int childIndex = int.Parse(child.name);

                foreach (Transform child1 in child)
                {
                    if(child1.childCount == 1)
                    {
                        if (spritesHelper == 0)
                        {
                            child1.GetComponent<Renderer>().material.mainTexture = source.images[enemiesData[childIndex].sprite1];
                        }
                        else
                        {
                            child1.GetComponent<Renderer>().material.mainTexture = source.images[enemiesData[childIndex].sprite2];
                        }
                    }
                }
            }
            if(spritesHelper == 0)
            {
                spritesHelper += 1;
            }
            else
            {
                spritesHelper = 0;
            }
            

            audioSource.clip = sounds[currentSound];
            audioSource.Play();
            if (currentSound == sounds.Length - 1)
            {
                currentSound = 0;
            }
            else
            {
                currentSound += 1;
            }

            foreach (Transform child in transform)
            {
                foreach (Transform child1 in child)
                {
                    if (Vector3.Distance(new Vector3(0, 0, child1.position.z), new Vector3(0, 0, player.position.z)) <= 0.5f && gameover == false)
                    {
                        GameObject.Find("gamePlay").GetComponent<GamePlayer>().GameOver();
                        gameover = true;
                    }
                }
            }
        }
        else
        {
            //Destroy(GameObject.Find("player"));
            //Destroy(gameObject);
            GameObject.Find("gamePlay").GetComponent<GamePlayer>().Winner();
        }
    }

    void Fire()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        int index = 0;
        int randon = UnityEngine.Random.Range(0, enemies.Length);
        foreach (GameObject enemy in enemies)
        {
            if (enemy.transform.parent.parent == transform)
            {
                if (index == randon)
                {
                    GameObject addBullet = Instantiate(bullet, enemy.transform.position, Quaternion.Euler(new Vector3(90, 180, 0)));
                    addBullet.tag = "enemyBullet";
                    addBullet.GetComponent<Bullet>().bulletSpeed = bulletSpeed;
                    //addBullet.GetComponent<Renderer>().material.mainTexture = source.images[enemiesData[int.Parse(enemy.transform.parent.name)].sprite4];
                    GameObject addPlayAudio = Instantiate(playerAudio);
                    addPlayAudio.GetComponent<PlayAudio>().PlayAudioClip(source.sounds[1]);
                    Destroy(addBullet, 5);
                }
                index += 1;
            }
        }
    }

    int DistanceSort(GameObject a, GameObject b)
    {
        return (transform.position - a.transform.position).sqrMagnitude.CompareTo((transform.position - b.transform.position).sqrMagnitude);
    }


}