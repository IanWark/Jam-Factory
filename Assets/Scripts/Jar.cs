using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Jar : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    public event Action<Jar> OnJarDeathEvent;

    private InputAction moveInput;

    private void FixedUpdate()
    {
        if (moveInput != null && moveInput.IsPressed())
        {
            Vector2 value = moveInput.ReadValue<Vector2>();
            transform.Translate(new Vector2(value.x * moveSpeed, 0));
        }
    }

    public void OnMove(InputAction input)
    {
        moveInput = input;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "KillingField")
        {
            OnJarDeathEvent.Invoke(this);
        }
    }
}
