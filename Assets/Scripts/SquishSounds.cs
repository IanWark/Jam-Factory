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

    public void PlayBigSquish()
    {
        int squishCount = Random.Range(4, 9);
        for (int i = 0; i < squishCount; i++)
        {
            PlaySquish();
        }
    }

    private void Start()
    {
        instance = this;
    }
}
