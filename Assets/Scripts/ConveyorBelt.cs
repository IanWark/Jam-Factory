using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(AudioSource))]
public class ConveyorBelt : MonoBehaviour
{
    public event Action<float> rollEvent;

    [SerializeField]
    private float pushMaxVelocity;

    [SerializeField]
    private float pushAcceleration;

    [SerializeField]
    private float stoppingAccelerationModifier;

    [SerializeField]
    private AudioClip conveyorAudioClip;

    [SerializeField]
    private bool alwaysOn = false;

    private float currentVelocity = 0f;

    private HashSet<Rigidbody2D> touchingRigidBodies = new HashSet<Rigidbody2D>();

    private InputAction moveAction;

    private AudioSource audioSource;
    private float audioPitch;
    private readonly float audioPitchNegativeVelocityBias = 0.1f; // slightly down-pitched if going left
    private readonly float audioPitchVelocityScale = 0.25f;
    private readonly Tuple<float,float> audioPitchMinMax = new Tuple<float, float>(0.9f, 1.3f);

    public void OnMove(InputAction.CallbackContext input)
    {
        moveAction = input.action;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = conveyorAudioClip;
        audioSource.loop = true;
        audioSource.Play();
    }

    private void Update()
    {
        audioPitch = Mathf.Clamp(
            Mathf.Abs(currentVelocity) * audioPitchVelocityScale - (currentVelocity < 0 ? audioPitchNegativeVelocityBias : 0),
            audioPitchMinMax.Item1,
            audioPitchMinMax.Item2);
        audioSource.pitch = audioPitch;
        audioSource.volume = Mathf.Clamp01(Mathf.Abs(currentVelocity));
    }

    private void FixedUpdate()
    {

        if (alwaysOn 
            || moveAction != null && moveAction.IsPressed())
        {
            float inputAmount = alwaysOn ? 1.0f : moveAction.ReadValue<Vector2>().x;

            currentVelocity = currentVelocity + inputAmount * Time.deltaTime * pushAcceleration;

            currentVelocity = Mathf.Clamp(currentVelocity, -pushMaxVelocity, pushMaxVelocity);

            rollEvent?.Invoke(currentVelocity);
        }
        else if (currentVelocity > 0)
        {
            currentVelocity = currentVelocity - Time.deltaTime * pushAcceleration * stoppingAccelerationModifier;

            currentVelocity = Mathf.Max(currentVelocity, 0);
        }
        else if (currentVelocity < 0)
        {
            currentVelocity = currentVelocity + Time.deltaTime * pushAcceleration * stoppingAccelerationModifier;

            currentVelocity = Mathf.Min(currentVelocity, 0);
        }

        touchingRigidBodies.RemoveWhere(IsNull);

        foreach (Rigidbody2D rigidbody in touchingRigidBodies)
        {
            Recipe recipe = rigidbody.GetComponent<Recipe>();
            if (recipe && recipe.hasAlreadyPostedScore)
            {
                continue;
            }

            rigidbody.linearVelocityX = currentVelocity;
        }
    }

    private static bool IsNull(Rigidbody2D rigidbody)
    {
        return rigidbody == null;
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
