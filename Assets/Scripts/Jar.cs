using System;
using UnityEngine;

public class Jar : MonoBehaviour
{
    public event Action<Jar> OnJarDeathEvent;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "KillingField")
        {
            OnJarDeathEvent.Invoke(this);
        }
    }
}
