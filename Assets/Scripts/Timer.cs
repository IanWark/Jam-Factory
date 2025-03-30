using System;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private float startTimeInSeconds = 180;

    [SerializeField]
    private float colouredTextTimeInSeconds = 30;

    [SerializeField]
    private Color colouredTextColour;

    [SerializeField]
    private TextMeshProUGUI text;

    public event Action OnGameOverEvent;

    private float currentTime;
    private bool gameEnded = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        currentTime = startTimeInSeconds;

        text.text = FormatTime(currentTime);
    }

    // Update is called once per frame
    private void Update()
    {
        if (gameEnded)
        {
            return;
        }

        currentTime -= Time.deltaTime;

        if (currentTime < colouredTextTimeInSeconds)
        {
            text.color = colouredTextColour;
        }
        if (currentTime <= 0)
        {
            EndGame();
        }

        text.text = FormatTime(currentTime);
    }

    private void EndGame()
    {
        gameEnded = true;
        text.text = FormatTime(0);

        OnGameOverEvent.Invoke();
    }

    private static string FormatTime(float time)
    {
        int minutes = (int)(time / 60);
        int seconds = (int)(time % 60);
        return string.Format("{0:0}:{1:00}", minutes, seconds);
    }
}
