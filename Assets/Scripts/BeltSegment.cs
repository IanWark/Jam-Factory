using UnityEngine;

public class BeltSegment : MonoBehaviour
{
    [SerializeField]
    private float spinFactor;

    [SerializeField]
    private GameObject myWheel;

	// to be set when spawning
    public ConveyorBelt belt;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
		if (belt)
		{
			belt.rollEvent += OnRoll;
        }
    }

	private void OnRoll(float rollAmount)
	{
		if (myWheel)
		{
			myWheel.transform.Rotate(new Vector3(0.0f,0.0f,-rollAmount * spinFactor));
		}
	}
}
