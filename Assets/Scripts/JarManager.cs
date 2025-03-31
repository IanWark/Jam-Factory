using System.Collections.Generic;
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

    private Transform leftMostJarTransform;

    private List<Jar> activeJars;
    private List<Jar> inactiveJars;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        activeJars = new List<Jar>(numJarsToSpawn);
        inactiveJars = new List<Jar>(numJarsToSpawn);

        for (int i = 0; i < numJarsToSpawn; ++i)
        {
            Jar newJar = Instantiate(jarPrefab, GetNewSpawnLocation(), Quaternion.identity);
            newJar.OnJarDeathEvent += OnJarDeath;

            activeJars.Add(newJar);

            leftMostJarTransform = newJar.transform;
        }
    }

    private void Update()
    {
        if (inactiveJars.Count > 0
            && leftMostJarTransform.position.x >= startLocation.position.x)
        {
            int index = inactiveJars.Count - 1;
            Jar jarToRespawn = inactiveJars[index];

            activeJars.Add(jarToRespawn);
            inactiveJars.RemoveAt(index);

            RespawnJar(jarToRespawn);
        }
    }

    private void RespawnJar(Jar jar)
    {
        jar.transform.position = GetNewSpawnLocation();

        jar.GetComponent<Recipe>().Respawn();

        leftMostJarTransform = jar.transform;

        jar.gameObject.SetActive(true);
    }

    private void OnJarDeath(Jar jar)
    {
        jar.gameObject.SetActive(false);
        inactiveJars.Add(jar);
        activeJars.Remove(jar);

        // Find leftMostJarTransform
        leftMostJarTransform = startLocation;
        foreach (Jar activeJar in activeJars)
        {
            if (activeJar.transform.position.x < leftMostJarTransform.transform.position.x)
            {
                leftMostJarTransform = activeJar.transform;
            }
        }
    }

    private Vector3 GetNewSpawnLocation()
    {
        Vector3 spawnLocation;

        if (leftMostJarTransform == null)
        {
            // Spawn first one forward so we can see it when the game starts
            return startLocation.transform.position;
        }
        if (leftMostJarTransform.transform.position.x > startLocation.transform.position.x)
        {
            spawnLocation = startLocation.transform.position;
        }
        else
        {
            spawnLocation = leftMostJarTransform.transform.position;
        }

        return new Vector3(spawnLocation.x - spaceBetweenJars, spawnLocation.y, spawnLocation.z);
    }
}
