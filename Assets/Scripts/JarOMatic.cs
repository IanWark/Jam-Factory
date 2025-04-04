using UnityEngine;

public class JarOMatic : MonoBehaviour
{
    [SerializeField]
    private Transform triggerLocation;

    [SerializeField]
    private float maxTriggerDistance;

    [SerializeField]
    private Squisher squisher;

    private GameObject jar = null;

    private bool locked = false;

    private void Start()
    {
        if (squisher)
        {
            squisher.resetAction += OnSquisherReset;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (jar == null && collision.gameObject.tag == "Jar")
        {
            Recipe recipe = collision.gameObject.GetComponent<Recipe>();
            if (recipe && !recipe.hasAlreadyPostedScore)
            {
                jar = collision.gameObject;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == jar)
        {
            jar = null;
        }
    }

    private void FixedUpdate()
    {
        if (jar && !locked)
        {
            //Debug.Log("Trigger distance = " + triggerDistance.ToString());
            float triggerDistance = Mathf.Abs(jar.transform.position.x - triggerLocation.position.x);

            if (triggerDistance < maxTriggerDistance)
            {
                LockAndLoad();
            }
        }
    }

    private void LockAndLoad()
    {
        // Lock jar in place
        locked = true;
        jar.transform.position = new Vector3(triggerLocation.position.x, jar.transform.position.y, jar.transform.position.z);

        Rigidbody2D body = jar.GetComponent<Rigidbody2D>();
        body.constraints |= RigidbodyConstraints2D.FreezePositionX;
        body.constraints |= RigidbodyConstraints2D.FreezePositionY;

        // Activate the squisher
        if (squisher)
        {
            squisher.ActivateSquish(jar);
        }
    }

    private void OnSquisherReset()
    {
        if (locked && jar)
        {
            Rigidbody2D body = jar.GetComponent<Rigidbody2D>();
            body.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
            body.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
            body.linearVelocityX = 7.0f;
            body.linearVelocityY = 6.0f;

            locked = false;
            jar = null;
        }
    }
}
