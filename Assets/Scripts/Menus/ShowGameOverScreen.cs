using UnityEngine;

public class ShowGameOverScreen : MonoBehaviour
{
    [SerializeField]
    private Timer timer;

    [SerializeField]
    private GameObject gameOverScreen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        timer.OnGameOverEvent += OnGameOver;
    }

    private void OnGameOver()
    {
        gameOverScreen.SetActive(true);
    }
}
