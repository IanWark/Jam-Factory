using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Recipe : MonoBehaviour
{
    public float[] percentArray;
    public TextMeshProUGUI percent1;
    public TextMeshProUGUI percent2;
    public TextMeshProUGUI percent3;
    public TextMeshProUGUI percent4;

    public TextMeshProUGUI actualPercent1;
    public TextMeshProUGUI actualPercent2;
    public TextMeshProUGUI actualPercent3;
    public TextMeshProUGUI actualPercent4;
    public Image image;

    public void Respawn()
    {
        Generate();
    }

    private void Start()
    {
        Generate();
    }

    private void Generate()
    {
        int numFruits = 4;
        percentArray = new float[numFruits];

        float totalPercent = 100.0f;
        for (int i = 0; i < numFruits - 1; i++)
        {
            percentArray[i] = Random.Range(0.0f, totalPercent);
            totalPercent -= percentArray[i];
        }

        percentArray[numFruits - 1] = totalPercent;

        percent1.text = Mathf.RoundToInt(percentArray[0]).ToString() + "%";
        percent2.text = Mathf.RoundToInt(percentArray[1]).ToString() + "%";
        percent3.text = Mathf.RoundToInt(percentArray[2]).ToString() + "%";
        percent4.text = Mathf.RoundToInt(percentArray[3]).ToString() + "%";
    }

    public void setScore(float score, float fullness, float[] fruitCount)
    {
        if (score <= 0.0)
            return;

        float totalCount = fruitCount[0] + fruitCount[1] + fruitCount[2] + fruitCount[3];
        Vector3 blue = new Vector3(0.0f, 0.0f, 1.0f);
        Vector3 red = new Vector3(1.0f, 0.0f, 0.0f);
        Vector3 green = new Vector3(0.0f, 1.0f, 0.0f);
        Vector3 yellow = new Vector3(1.0f, 1.0f, 0.0f);

        Vector3 finalColour = ((blue * fruitCount[0]) + (green * fruitCount[1]) + (yellow * fruitCount[2]) + (red * fruitCount[3]))/totalCount;
        image.color = new Color(finalColour.x, finalColour.y, finalColour.z, 0.75f);
        image.transform.localScale = new Vector3(1.0f, fullness, 1.0f);
        image.transform.position -= new Vector3(0.0f,(1.0f-fullness)*0.5f, 0.0f);

        actualPercent1.enabled = true;
        actualPercent2.enabled = true;
        actualPercent3.enabled = true;
        actualPercent4.enabled = true;

        actualPercent1.text = Mathf.RoundToInt(fruitCount[0] / totalCount * 100.0f).ToString() + "%";
        actualPercent2.text = Mathf.RoundToInt(fruitCount[1] / totalCount * 100.0f).ToString() + "%";
        actualPercent3.text = Mathf.RoundToInt(fruitCount[2] / totalCount * 100.0f).ToString() + "%";
        actualPercent4.text = Mathf.RoundToInt(fruitCount[3] / totalCount * 100.0f).ToString() + "%";
    }
}
