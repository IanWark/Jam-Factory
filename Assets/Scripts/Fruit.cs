using UnityEngine;

public class Fruit : MonoBehaviour
{
    public int id;

    [SerializeField]
    private float timeUntilOld = 1;

    [SerializeField]
    private Color particleColour;

    private float creationTime;

    public GameObject particleSystemPrefab;

    private void Start()
    {
        creationTime = Time.time;
    }

    public bool IsOld()
    {
        return Time.time > creationTime + timeUntilOld;
    }

    public void DestroyIt()
    {
        SquishSounds.instance.PlaySquish();

        ParticleSystem.MainModule main = Instantiate(particleSystemPrefab, transform.position, transform.rotation).GetComponent<ParticleSystem>().main;
        main.startColor = particleColour;

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Fruit" && IsOld())
        {
            DestroyIt();
        }
    }
}
