using UnityEngine;

public class Squisher : MonoBehaviour
{
    public KeyCode DebugKeyCode;
    private float NormalHeight;
    public float CompressedHeight;
    public float DownSpeed;
    public float UpSpeed;

    private bool isSquishing = false;

    private void Start()
    {
        NormalHeight = transform.position.y;
    }

    private void Update()
    {
        isSquishing = false;
        if(Input.GetKey(DebugKeyCode))
        {
            if(transform.position.y > CompressedHeight)
            {
                transform.position -= new Vector3(0f, DownSpeed * Time.deltaTime, 0f);
                isSquishing = true;
            } else
            {
                transform.position = new Vector3(transform.position.x, CompressedHeight, transform.position.z);
            }
        }
        else
        {
            if (transform.position.y < NormalHeight)
            {
                transform.position += new Vector3(0f, UpSpeed * Time.deltaTime, 0f);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, NormalHeight, transform.position.z);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isSquishing)
        {
            if (collision.gameObject.tag == "Fruit")
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
