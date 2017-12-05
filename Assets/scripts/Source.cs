using UnityEngine;

[CreateAssetMenu (menuName = "ScriptsbleObjects/Source")]
public class Source : ScriptableObject
{
    public Texture2D[] images;
    public Color[] colors;
    public AudioClip[] sounds;
}