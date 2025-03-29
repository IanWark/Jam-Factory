using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Jar : MonoBehaviour
{
    public event Action<Jar> OnJarDeathEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "KillingField")
        {
            OnJarDeathEvent.Invoke(this);
        }
    }
}
