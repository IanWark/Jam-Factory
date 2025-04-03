using UnityEngine;
using UnityEngine.InputSystem;

public class MenuInputManager : MonoBehaviour
{
    [SerializeField]
    private StartGame startGame;

    [SerializeField]
    private QuitGame quitGame;

    public void OnStartGame(InputAction.CallbackContext input)
    {
        startGame.Go();
    }
    public void OnQuitGame(InputAction.CallbackContext input)
    {
        quitGame.Quit();
    }
}
