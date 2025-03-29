using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ConveyorBelt : MonoBehaviour
{
    public event Action<float> rollEvent;

    [SerializeField]
    private float pushMaxVelocity;

    [SerializeField]
    private float pushAcceleration;

    [SerializeField]
    private float stoppingAccelerationModifier;

    private float currentVelocity = 0f;

    private HashSet<Rigidbody2D> touchingRigidBodies = new HashSet<Rigidbody2D>();

    private InputAction moveAction;

    private void FixedUpdate()
    {
        if (moveAction != null && moveAction.IsPressed())
        {
            float inputAmount = moveAction.ReadValue<Vector2>().x;
            currentVelocity = currentVelocity + inputAmount * Time.deltaTime * pushAcceleration;

            currentVelocity = Mathf.Clamp(currentVelocity, -pushMaxVelocity, pushMaxVelocity);

            rollEvent?.Invoke(inputAmount);

            Debug.Log($"Go! {currentVelocity}");
        }
        else if (currentVelocity > 0)
        {
            currentVelocity = currentVelocity - Time.deltaTime * pushAcceleration * stoppingAccelerationModifier;

            currentVelocity = Mathf.Max(currentVelocity, 0);

            Debug.Log($"Slow! {currentVelocity}");
        }
        else if (currentVelocity < 0)
        {
            currentVelocity = currentVelocity + Time.deltaTime * pushAcceleration * stoppingAccelerationModifier;

            currentVelocity = Mathf.Min(currentVelocity, 0);

            Debug.Log($"Slow! {currentVelocity}");
        }

        foreach (Rigidbody2D rigidbody in touchingRigidBodies)
        {
            rigidbody.linearVelocityX = currentVelocity;
        }
    }

    public void OnMove(InputAction.CallbackContext input)
    {
        moveAction = input.action;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D collidingRigidBody = collision.gameObject.GetComponent<Rigidbody2D>();
        if (collidingRigidBody != null)
        {
            touchingRigidBodies.Add(collidingRigidBody);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Rigidbody2D collidingRigidBody = collision.gameObject.GetComponent<Rigidbody2D>();
        if (collidingRigidBody != null)
        {
            touchingRigidBodies.Remove(collidingRigidBody);
        }
    }
}
