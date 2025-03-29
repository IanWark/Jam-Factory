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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        Vector3 spawnLocation = startLocation.position;
        for (int i = 0; i < numJarsToSpawn; ++i)
        {
            Jar newJar = Instantiate(jarPrefab, spawnLocation, Quaternion.identity);
            newJar.OnJarDeathEvent += RespawnJar;

            spawnLocation = new Vector3(spawnLocation.x - spaceBetweenJars, spawnLocation.y, spawnLocation.z);
        }
    }

    private void RespawnJar(Jar jar)
    {
        
    }
}
