using UnityEngine;

public class JarManager : MonoBehaviour
{
    [SerializeField]
    private int numJarsToSpawn;

    [SerializeField]
    private float spaceBetweenJars;

    [Header("References")]

    [SerializeField]
    private Transform startLocation;

    [SerializeField]
    private Jar jarPrefab;

    private Jar lastSpawnedJar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        for (int i = 0; i < numJarsToSpawn; ++i)
        {
            Jar newJar = Instantiate(jarPrefab, GetNewSpawnLocation(), Quaternion.identity);
            newJar.OnJarDeathEvent += RespawnJar;

            lastSpawnedJar = newJar;
        }
    }

    private void RespawnJar(Jar jar)
    {
        jar.transform.position = GetNewSpawnLocation();

        jar.GetComponent<Recipe>().Respawn();

        lastSpawnedJar = jar;
    }

    private Vector3 GetNewSpawnLocation()
    {
        Vector3 spawnLocation;

        if (lastSpawnedJar == null)
        {
            // Spawn first one forward so we can see it when the game starts
            return startLocation.transform.position;
        }
        if (lastSpawnedJar.transform.position.x > startLocation.transform.position.x)
        {
            spawnLocation = startLocation.transform.position;
        }
        else
        {
            spawnLocation = lastSpawnedJar.transform.position;
        }

        return new Vector3(spawnLocation.x - spaceBetweenJars, spawnLocation.y, spawnLocation.z);
    }
}
