using UnityEngine;

public class KillPlane : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Fruit")
        {
            Destroy(collision.gameObject);
        }
    }
}
