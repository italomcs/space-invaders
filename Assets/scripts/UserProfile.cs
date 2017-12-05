using UnityEngine;

public class UserProfile : MonoBehaviour
{
    public static User userProfile;

    private void Awake()
    {
        if(userProfile == null)
        {
            userProfile = new User();
        }
    }
}