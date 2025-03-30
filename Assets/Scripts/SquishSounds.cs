using UnityEngine;

public class SquishSounds : MonoBehaviour
{
    public static SquishSounds instance;

    [SerializeField]
    private AudioClip[] squishClips;

    [SerializeField]
    private AudioSource audioSource;

    public void PlaySquish()
    {
        audioSource.PlayOneShot(squishClips[Random.Range(0, squishClips.Length)]);
    }

    private void Start()
    {
        instance = this;
    }
}
