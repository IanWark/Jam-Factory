using UnityEngine;

public class InputManagerManager : MonoBehaviour
{
    [SerializeField]
    private Timer timer;

    [SerializeField]
    private GameObject gameInput;

    [SerializeField]
    private GameObject menuInput;

    private void Start()
    {
        timer.OnGameOverEvent += OnGameOver;
    }

    private void OnGameOver()
    {
        gameInput.SetActive(false);
        menuInput.SetActive(true);
    }
}
