using UnityEngine;

public class Fruit : MonoBehaviour
{
    public int id;

    [SerializeField]
    private float timeUntilOld = 1;

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
        Vector3[] colours = new Vector3[4];
        colours[0] = new Vector3(0.0f, 0.0f, 1.0f);
        colours[1] = new Vector3(0.0f, 1.0f, 0.0f);
        colours[2] = new Vector3(1.0f, 1.0f, 0.0f);
        colours[3] = new Vector3(1.0f, 0.0f, 0.0f);
        ParticleSystem.MainModule main = Instantiate(particleSystemPrefab, transform.position, transform.rotation).GetComponent<ParticleSystem>().main;
        main.startColor = new Color(colours[id].x, colours[id].y, colours[id].z);

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
