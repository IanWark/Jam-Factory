using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    // Is this terrible code? Yes. Do I care? no.
    public TextMeshProUGUI Moeny;
    public static float score;

    [SerializeField]
    private TextMeshProUGUI finalScoreText;

    [SerializeField]
    private Timer timer;

    private void Start()
    {
        timer.OnGameOverEvent += OnGameOver;
    }

    private void Update()
    {
        Moeny.text = GetFormattedScore();
    }

    private void OnGameOver()
    {
        finalScoreText.text = GetFormattedScore();
    }

    private string GetFormattedScore()
    {
        return string.Format("${0:0.00}", score);
    }

    public static void IncreaseScore(float jamJarValue)
    {
        score += jamJarValue;
        score = Mathf.Round(score * 100f) / 100f;
    }

    public static void ResetScore() { score = 0; }
}
