using UnityEngine;

[CreateAssetMenu(fileName = "FruitColour", menuName = "ScriptableObjects/Fruit Colour Object", order = 1)]
public class FruitColourObject : ScriptableObject
{
    [SerializeField]
    public Color colour;
}
