using System;
using System.Collections.Generic;

[Serializable]
public class Users
{
    public List<User> list;
}

[Serializable]
public class User
{
    public string name;
    public string email;
    public int score;
}

[Serializable]
public class Settings
{
    public PlayerData player;
    public List<Enemy> enemies;
    public List<Difficulty> difficulties;
    public int currentDifficulty;
    public int background;
    public int startScore;
    public int gameTime;
    public string introText;
    public int startTitle;
}

[Serializable]
public class PlayerData
{
    public int lives;
    public int sprite;
    public int sprite1;
    public int bulletSprite;
    public float fireSpeed;
    public float bulletSpeed;
}

[Serializable]
public class Enemy
{
    public string title;
    public int lives;
    public int score;
    public int sprite1;
    public int sprite2;
    public int sprite3;
    public int sprite4;
    public int deathSound;
}

[Serializable]
public class Difficulty
{
    public string title;
    public int lives;
    public float refreshMovement;
    public float refreshSpeed;
    public float decrasePercent;
    public float fireFrequency;
    public float bulletSpeed;
}