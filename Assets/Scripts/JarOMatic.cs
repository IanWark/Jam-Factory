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
            squisher.resetAction += OnSquisherReset();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (jar == null && collision.gameObject.tag == "Jar")
        {
            jar = collision.gameObject;
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
            squisher.ActivateSquish();
        }
    }

    private void OnSquisherReset()
    {
        if (locked)
        {
            locked = false;
            Rigidbody2D body = jar.GetComponent<Rigidbody2D>();
            body.constraints &= !RigidbodyConstraints2D.FreezePositionX;
            body.constraints &= !RigidbodyConstraints2D.FreezePositionY;
        }

        if (jar)
        {

        }
    }
}