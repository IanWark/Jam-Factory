using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private PlayerInput playerInput;

    [SerializeField]
    private Timer timer;

    [SerializeField]
    private ConveyorBelt conveyorBelt;

    [SerializeField]
    private Pipe bluePipe;

    [SerializeField]
    private Pipe greenPipe;

    [SerializeField]
    private Pipe yellowPipe;

    [SerializeField]
    private Pipe redPipe;

    [SerializeField]
    private Squisher squisher;

    private void Start()
    {
        timer.OnGameOverEvent += OnGameOver;
    }

    private void OnGameOver()
    {
        playerInput.enabled = false;
    }

    public void OnMove(InputAction.CallbackContext input)
    {
        conveyorBelt.OnMove(input);
    }

    public void OnBlueActivate(InputAction.CallbackContext input)
    {
        bluePipe.OnActivate(input);
    }

    public void OnGreenActivate(InputAction.CallbackContext input)
    {
        greenPipe.OnActivate(input);
    }

    public void OnYellowActivate(InputAction.CallbackContext input)
    {
        yellowPipe.OnActivate(input);
    }

    public void OnRedActivate(InputAction.CallbackContext input)
    {
        redPipe.OnActivate(input);
    }

    public void OnSquisherActivate(InputAction.CallbackContext input)
    {
        squisher.OnActivate(input);
    }
}
