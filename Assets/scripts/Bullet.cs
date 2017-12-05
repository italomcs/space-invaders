using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;

    void Update ()
    {
        transform.Translate(-transform.forward*bulletSpeed*Time.deltaTime);
    }
}