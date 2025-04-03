using UnityEngine;

public class Fruit : MonoBehaviour
{
    private const float MAX_FRUIT_PARTICLE_MASS = 0.4f;
    private const float FRUIT_PARTICLE_RATIO_MULTIPLIER = 200;

    public int id;

    [SerializeField]
    private float timeUntilOld = 1;

    [SerializeField]
    private FruitColourObject particleColour;

    private float creationTime;

    private bool beingDestroyed = false;

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
        beingDestroyed = true;

        SquishSounds.instance.PlaySquish();

        Rigidbody2D rigidbody = this.GetComponent<Rigidbody2D>();

        ParticleSystem particleSystem = Instantiate(particleSystemPrefab, transform.position, transform.rotation).GetComponent<ParticleSystem>();

        ParticleSystem.MainModule main = particleSystem.main;
        main.startColor = particleColour.colour;

        float massRatio = rigidbody.mass / MAX_FRUIT_PARTICLE_MASS;
        Debug.Log($"name: {name} scale: {transform.localScale} mass: {rigidbody.mass} / maxMass {MAX_FRUIT_PARTICLE_MASS} = massRatio {massRatio}");
        ParticleSystem.EmissionModule emission = particleSystem.emission;
        emission.rateOverTime = massRatio * FRUIT_PARTICLE_RATIO_MULTIPLIER;

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!beingDestroyed 
            && (collision.gameObject.tag == "Fruit" || collision.gameObject.tag == "Jar")
            && IsOld())
        {
            DestroyIt();
        }
    }
}
