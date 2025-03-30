using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    // Is this terrible code? Yes. Do I care? no.
    public TextMeshProUGUI Moeny;
    public static float score;

    private void Update()
    {
        Moeny.text = "$" + score;
    }

    public static void IncreaseScore(float jamJarValue)
    {
        score += jamJarValue;
        score = Mathf.Round(score * 100f) / 100f;
    }

    public static void ResetScore() { score = 0; }
}
