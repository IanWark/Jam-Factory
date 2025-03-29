using UnityEngine;

public class BeltSegmentSpawner : MonoBehaviour
{
    [SerializeField]
    private int numSegmentsToSpawn;

    [SerializeField]
    private float spaceBetweenSegments;

    [Header("References")]

    [SerializeField]
    private ConveyorBelt conveyorBelt;

    [SerializeField]
    private Transform startLocation;

    [SerializeField]
    private BeltSegment beltSegmentPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        Vector3 spawnLocation = startLocation.position;
        for (int i = 0; i < numSegmentsToSpawn; ++i)
        {
            BeltSegment newSegment = Instantiate(beltSegmentPrefab, spawnLocation, Quaternion.identity);
            newSegment.belt = conveyorBelt;
            spawnLocation = new Vector3(spawnLocation.x - spaceBetweenSegments, spawnLocation.y, spawnLocation.z);
        }
    }
}
