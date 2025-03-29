using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ConveyorBelt : MonoBehaviour
{

    [SerializeField]
    private float pushVelocity;

    private HashSet<Rigidbody2D> touchingRigidBodies = new HashSet<Rigidbody2D>();

    private InputAction moveAction;

    private void FixedUpdate()
    {
        if (moveAction != null && moveAction.IsPressed())
        {
            foreach (Rigidbody2D rigidbody in touchingRigidBodies)
            {
                rigidbody.linearVelocityX = moveAction.ReadValue<Vector2>().x * pushVelocity;
            }
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D collidingRigidBody = collision.gameObject.GetComponent<Rigidbody2D>();
        if (collidingRigidBody != null)
        {
            touchingRigidBodies.Add(collidingRigidBody);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Rigidbody2D collidingRigidBody = collision.gameObject.GetComponent<Rigidbody2D>();
        if (collidingRigidBody != null)
        {
            touchingRigidBodies.Remove(collidingRigidBody);
        }
    }
}
