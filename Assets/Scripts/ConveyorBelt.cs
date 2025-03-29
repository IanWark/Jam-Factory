using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ConveyorBelt : MonoBehaviour
{
    public event Action<float> rollEvent;

    [SerializeField]
    private float pushVelocity;

    private HashSet<Rigidbody2D> touchingRigidBodies = new HashSet<Rigidbody2D>();

    private InputAction moveAction;

    private void FixedUpdate()
    {
        if (moveAction != null && moveAction.IsPressed())
        {
            float inputAmount = moveAction.ReadValue<Vector2>().x;

            foreach (Rigidbody2D rigidbody in touchingRigidBodies)
            {
                rigidbody.linearVelocityX = inputAmount * pushVelocity;
            }

            rollEvent?.Invoke(inputAmount);
        }
        else
        {
            foreach (Rigidbody2D rigidbody in touchingRigidBodies)
            {
                rigidbody.linearVelocityX = 0;
            }
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
