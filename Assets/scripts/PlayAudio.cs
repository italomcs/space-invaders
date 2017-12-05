using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClip;

    private void Update()
    {
        if(audioSource.isPlaying == false)
        {
            Destroy(gameObject);
        }
    }

    public void PlayAudioClip(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}