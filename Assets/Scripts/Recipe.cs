using UnityEngine;

public class Recipe : MonoBehaviour
{

    public float[] percentArray;

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
    }
}
