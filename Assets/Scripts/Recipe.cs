using UnityEngine;
using TMPro;

public class Recipe : MonoBehaviour
{
    public float[] percentArray;
    public TextMeshProUGUI percent1;
    public TextMeshProUGUI percent2;
    public TextMeshProUGUI percent3;
    public TextMeshProUGUI percent4;

    private void Start()
    {
        int numFruits = 4;
        percentArray = new float[numFruits];

        float totalPercent = 100.0f; 
        for(int i = 0; i < numFruits-1; i++)
        {
            percentArray[i] = Random.Range(0.0f, totalPercent);
            totalPercent -= percentArray[i];
        }

        percentArray[numFruits-1] = totalPercent;

        percent1.text = Mathf.RoundToInt(percentArray[0]).ToString() + "%";
        percent2.text = Mathf.RoundToInt(percentArray[1]).ToString() + "%";
        percent3.text = Mathf.RoundToInt(percentArray[2]).ToString() + "%";
        percent4.text = Mathf.RoundToInt(percentArray[3]).ToString() + "%";
    }
}
