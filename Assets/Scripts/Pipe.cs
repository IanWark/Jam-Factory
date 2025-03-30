using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(AudioSource))]
public class Pipe : MonoBehaviour
{
    public GameObject FruitToSpawn;
    public Transform SpawnPoint;
    public float SpawnsPerSecond;
    public float SpawnScaleMin;
    public float SpawnScaleMax;
    [SerializeField]
    private float spawnPosMin;
    [SerializeField]
    private float spawnPosMax;
    public float HorizontalForceMin;
    public float HorizontalForceMax;
    public float VerticalForceMin;
    public float VerticalForceMax;

    [SerializeField]
    private Collider2D pipeCollider;

    [SerializeField]
    private AudioClip fruitSpawnSound;
    private AudioSource audioSource;

    private float TimeSinceLastSpawn = 0.0f;
    private float WhenToUpdate = 0.0f;

    private InputAction activateAction;

    public void OnActivate(InputAction.CallbackContext input)
    {
        TimeSinceLastSpawn = WhenToUpdate;

        activateAction = input.action;
    }

    void Start()
    {
        WhenToUpdate = 1.0f / SpawnsPerSecond;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        TimeSinceLastSpawn += Time.deltaTime;

        if (activateAction != null && activateAction.IsPressed()
            && TimeSinceLastSpawn > WhenToUpdate
            && !IsBlocked())
        {
            float positionModifier = Random.Range(spawnPosMin, spawnPosMax);
            Vector3 modifiedSpawnPosition = new Vector3(SpawnPoint.position.x + positionModifier, SpawnPoint.position.y, SpawnPoint.position.z);

            GameObject Fruit = Instantiate(FruitToSpawn, modifiedSpawnPosition, SpawnPoint.rotation);
            audioSource.pitch = Random.Range(0.9f,1.6f);
            audioSource.PlayOneShot(fruitSpawnSound);

            float Scale = Random.Range(SpawnScaleMin, SpawnScaleMax);
            Fruit.transform.localScale = new Vector3(Scale, Scale, Scale);
            Fruit.transform.Rotate(new Vector3(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
            Rigidbody2D rb2d = Fruit.GetComponent<Rigidbody2D>();
            rb2d.AddForce(new Vector2(Random.Range(HorizontalForceMin, HorizontalForceMax), Random.Range(VerticalForceMin, VerticalForceMax)));

            TimeSinceLastSpawn = 0.0f;
        }
    }

    private bool IsBlocked()
    {
        List<RaycastHit2D> results = new();
        pipeCollider.Cast(new Vector2(0,0), results);

        foreach (RaycastHit2D result in results)
        {
            Fruit fruit = result.collider.GetComponent<Fruit>();
            if (fruit != null && fruit.IsOld())
            {
                return true;
            }
        }

        return false;
    }
}
