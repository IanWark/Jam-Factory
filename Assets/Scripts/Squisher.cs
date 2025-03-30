using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Squisher : MonoBehaviour
{
    public KeyCode DebugKeyCode;
    private float NormalHeight;
    public float CompressedHeight;
    public float DownSpeed;
    public float UpSpeed;

    public float jarWidth = 1.0f;
    public float expectedMass = 0.5f;
    private bool isSquishing = false;
    private GameObject jar;

    private AudioSource audioSource;

    [SerializeField]
    private AudioClip startClip;
    [SerializeField]
    private AudioClip impactClip;

    [SerializeField]
    private AudioClip[] squishClips;


    private bool canPlayStartSound = true;
    private bool canPlayImpactSound = true;

    bool hasScorredJar = false;
    private float totalMass;
    private float[] fruitCount = new float[4];

    private void Start()
    {
        NormalHeight = transform.position.y;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        isSquishing = false;
        if(Input.GetKey(DebugKeyCode))
        {
            if (canPlayStartSound)
            {
                audioSource.PlayOneShot(startClip);
                canPlayStartSound = false;
            }

            if(transform.position.y > CompressedHeight)
            {
                transform.position -= new Vector3(0f, DownSpeed * Time.deltaTime, 0f);
                isSquishing = true;
            } else
            {
                if(canPlayImpactSound)
                {
                    audioSource.PlayOneShot(impactClip);
                    canPlayImpactSound = false;
                }

                transform.position = new Vector3(transform.position.x, CompressedHeight, transform.position.z);
                if(!hasScorredJar && jar != null)
                {
                    hasScorredJar = true;
                    Recipe recipe = jar.GetComponent<Recipe>();

                    float totalMass = fruitCount[0] + fruitCount[1] + fruitCount[2] + fruitCount[3];
                    if (totalMass > 0)
                    {
                        float fullness = Mathf.Min(1.0f, totalMass / expectedMass);
                        float Ab = Mathf.Min(recipe.percentArray[0] / 100.0f, fruitCount[0] / totalMass);
                        float As = Mathf.Min(recipe.percentArray[1] / 100.0f, fruitCount[1] / totalMass);
                        float Aa = Mathf.Min(recipe.percentArray[2] / 100.0f, fruitCount[2] / totalMass);
                        float Ar = Mathf.Min(recipe.percentArray[3] / 100.0f, fruitCount[3] / totalMass);
                        float accuracy = Ab + As + Aa + Ar;
                        float score = 10.0f * fullness * accuracy;

                        recipe.setScore(score, fullness, fruitCount);
                        audioSource.PlayOneShot(squishClips[Random.Range(0, squishClips.Length)]);
                        Debug.Log(totalMass);
                    }
                }
            }
        }
        else
        {
            hasScorredJar = false;
            if (transform.position.y < NormalHeight)
            {
                transform.position += new Vector3(0f, UpSpeed * Time.deltaTime, 0f);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, NormalHeight, transform.position.z);
            }

            canPlayStartSound = true;
            canPlayImpactSound = true;
        }

        if (!isSquishing)
        {
            jar = null;
            fruitCount = new float[4];
            totalMass = 0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isSquishing)
        {
            if(jar == null && collision.gameObject.tag == "Jar")
            {
                jar = collision.gameObject;
            }
            if (collision.gameObject.tag == "Fruit")
            {
                if(jar != null &&
                    Mathf.Abs(jar.transform.position.x - collision.gameObject.transform.position.x) < (jarWidth / 2.0f))
                {
                    float mass = collision.gameObject.GetComponent<Rigidbody2D>().mass;
                    totalMass += collision.gameObject.GetComponent<Rigidbody2D>().mass;
                    fruitCount[collision.gameObject.GetComponent<Fruit>().id] += mass;
                }
                Destroy(collision.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isSquishing)
        {
            if (jar == null && collision.gameObject.tag == "Jar")
            {
                jar = collision.gameObject;
                Debug.Log("Jar!");
            }
        }
    }
}
