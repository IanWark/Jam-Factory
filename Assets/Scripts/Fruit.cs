using UnityEngine;

public class Fruit : MonoBehaviour
{
    public int id;

    [SerializeField]
    private float timeUntilOld = 1;

    private float creationTime;

    private void Start()
    {
        creationTime = Time.time;
    }

    public bool IsOld()
    {
        return Time.time > creationTime + timeUntilOld;
    }
}
