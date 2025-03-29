using UnityEngine;

public class Pipe : MonoBehaviour
{
    public GameObject FruitToSpawn;
    public float SpawnsPerSecond;
    public float SpawnScaleMin;
    public float SpawnScaleMax;
    public float HorizontalForceMin;
    public float HorizontalForceMax;
    public float VerticalForceMin;
    public float VerticalForceMax;
    public KeyCode debugActivateKey;

    private float TimeSinceLastSpawn = 0.0f;
    private float WhenToUpdate = 0.0f;
    void Start()
    {
        WhenToUpdate = SpawnsPerSecond / 60.0f; //??
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(debugActivateKey)) 
        {
            TimeSinceLastSpawn += Time.deltaTime;
            if(TimeSinceLastSpawn > WhenToUpdate) 
            {
                GameObject fruit = Instantiate(FruitToSpawn).GetComponent<Fruit>();
                fruit.Scale = Random.Range(SpawnScaleMin, SpawnScaleMax);
                fruit.transform.localScale = new Vector3(fruit.Scale, fruit.Scale, fruit.Scale);

                fruit.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(HorizontalForceMin, HorizontalForceMax), Random.Range(VerticalForceMin, VerticalForceMax)));
            }
        }
    }
}
