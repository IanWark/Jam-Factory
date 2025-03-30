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

    private float currentTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentTime = startTimeInSeconds;

        text.text = FormatTime(currentTime);
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;

        if (currentTime < colouredTextTimeInSeconds)
        {
            text.color = colouredTextColour;
        }

        text.text = FormatTime(currentTime);
    }

    private static string FormatTime(float time)
    {
        int minutes = (int)(time / 60);
        int seconds = (int)(time % 60);
        return string.Format("{0:0}:{1:00}", minutes, seconds);
    }
}
