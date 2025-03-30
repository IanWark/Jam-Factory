using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(AudioSource))]
public class Squisher : MonoBehaviour
{
    public event Action resetAction;

    private bool needsReset = false;

    private float NormalHeight;
    public float CompressedHeight;
    public float DownSpeed;
    public float UpSpeed;

    private bool isSquishing = false;
    public float jarWidth = 1.0f;
    public float expectedMass = 0.5f;
    private GameObject jar;

    public GameObject squishParticleSystem;

    private AudioSource audioSource;

    [SerializeField]
    private AudioClip startClip;
    [SerializeField]
    private AudioClip impactClip;

    private bool canPlayStartSound = true;
    private bool canPlayImpactSound = true;

    bool hasScorredJar = false;
    private float totalMass;
    private float[] fruitCount = new float[4];

    private InputAction activateAction;

    public void OnActivate(InputAction.CallbackContext input)
    {
        activateAction = input.action;
    }

    private void Start()
    {
        NormalHeight = transform.position.y;
        audioSource = GetComponent<AudioSource>();
    }

    public void ActivateSquish()
    {
        isSquishing = true;
        audioSource.PlayOneShot(startClip);
    }

    private void Update()
    {
        if (isSquishing)
        {
            if(transform.position.y > CompressedHeight)
            {
                transform.position -= new Vector3(0f, DownSpeed * Time.deltaTime, 0f);
            }
            else
            {
                audioSource.PlayOneShot(impactClip);
                isSquishing = false;
                needsReset = true;

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
                    }
                    else
                    {
                        float score = 0.0f;
                        float fullness = 0.0f;
                        recipe.setScore(score, fullness, fruitCount);
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
            else if (needsReset)
            {
                transform.position = new Vector3(transform.position.x, NormalHeight, transform.position.z);

                resetAction.Invoke();
                jar = null;
                fruitCount = new float[4];
                totalMass = 0f;

                needsReset = false;
            }

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
                if (jar != null &&
                    Mathf.Abs(jar.transform.position.x - collision.gameObject.transform.position.x) < (jarWidth / 2.0f))
                {
                    float mass = collision.gameObject.GetComponent<Rigidbody2D>().mass;
                    totalMass += mass;
                    fruitCount[collision.gameObject.GetComponent<Fruit>().id] += mass;
                }
                else
                {
                    collision.gameObject.GetComponent<Fruit>().DestroyIt();
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
