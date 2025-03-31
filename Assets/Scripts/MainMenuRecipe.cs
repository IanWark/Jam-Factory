using UnityEngine;
using System.Linq;

public class MainMenuRecipe : Recipe
{
    [SerializeField]
    private float MultipleUsedToDetermineActual;

    [SerializeField]
    private float fullnessMin;
    [SerializeField]
    private float fullnessMax;

    public override void Respawn()
    {
        Generate();
    }

    private void Start()
    {
        Generate();
    }

    private void Generate()
    {
        //This is all tremendously stupid, but it'll work
        image.enabled = false;

        int numFruits = 4;

        float totalPercent = 100.0f;

        // Generate expected percents
        float[] expectedPercentArray = new float[numFruits];

        int[] expectedRandomOrder = new int[numFruits];
        for (int i = 0; i < numFruits - 1; i++)
        {
            expectedRandomOrder[i] = 5;
        }

        for (int i = 0; i < numFruits - 1; i++)
        {
            int randomNumber = 5;
            while (expectedRandomOrder.Contains(randomNumber))
            {
                randomNumber = Random.Range(0, numFruits);
            }
            expectedRandomOrder[i] = randomNumber;
        }

        for (int i = 0; i < numFruits - 1; i++)
        {
            expectedPercentArray[expectedRandomOrder[i]] = Mathf.RoundToInt(Random.Range(0.0f, totalPercent / MultipleUsedToDetermineRecipe)) * MultipleUsedToDetermineRecipe;
            totalPercent -= expectedPercentArray[expectedRandomOrder[i]];
        }

        expectedPercentArray[expectedRandomOrder[numFruits - 1]] = totalPercent;

        percent1.text = Mathf.RoundToInt(expectedPercentArray[0]).ToString() + "%";
        percent2.text = Mathf.RoundToInt(expectedPercentArray[1]).ToString() + "%";
        percent3.text = Mathf.RoundToInt(expectedPercentArray[2]).ToString() + "%";
        percent4.text = Mathf.RoundToInt(expectedPercentArray[3]).ToString() + "%";

        // Generate actual percent and set actual percents and colour
        totalPercent = 100.0f;

        float[] actualPercentArray = new float[numFruits];

        int[] actualRandomOrder = new int[numFruits];
        for (int i = 0; i < numFruits - 1; i++)
        {
            actualRandomOrder[i] = 5;
        }

        for (int i = 0; i < numFruits - 1; i++)
        {
            int randomNumber = 5;
            while (actualRandomOrder.Contains(randomNumber))
            {
                randomNumber = Random.Range(0, numFruits);
            }
            actualRandomOrder[i] = randomNumber;
        }

        for (int i = 0; i < numFruits - 1; i++)
        {
            actualPercentArray[expectedRandomOrder[i]] = Mathf.RoundToInt(Random.Range(0.0f, totalPercent / MultipleUsedToDetermineActual)) * MultipleUsedToDetermineActual;
            totalPercent -= actualPercentArray[actualRandomOrder[i]];
        }

        actualPercentArray[actualRandomOrder[numFruits - 1]] = totalPercent;

        float totalCount = actualPercentArray[0] + actualPercentArray[1] + actualPercentArray[2] + actualPercentArray[3];

        Color blue = blueberryColour.colour;
        Color red = raspberryColour.colour;
        Color green = appleColour.colour;
        Color yellow = apricotColour.colour;

        Color finalColour = ((blue * actualPercentArray[0]) + (green * actualPercentArray[1]) + (yellow * actualPercentArray[2]) + (red * actualPercentArray[3])) / totalCount;

        image.enabled = true;
        image.color = new Color(finalColour.r, finalColour.g, finalColour.b, jamAlpha);

        float fullness = Random.Range(fullnessMin, fullnessMax);
        image.transform.localScale = new Vector3(1.0f, fullness, 1.0f);

        actualPercent1.text = Mathf.RoundToInt(actualPercentArray[0]).ToString() + "%";
        actualPercent2.text = Mathf.RoundToInt(actualPercentArray[1]).ToString() + "%";
        actualPercent3.text = Mathf.RoundToInt(actualPercentArray[2]).ToString() + "%";
        actualPercent4.text = Mathf.RoundToInt(actualPercentArray[3]).ToString() + "%";

        actualPercent1.enabled = true;
        actualPercent2.enabled = true;
        actualPercent3.enabled = true;
        actualPercent4.enabled = true;
    }
}
