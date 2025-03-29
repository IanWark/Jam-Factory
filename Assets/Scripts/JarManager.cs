using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    private List<Jar> createdJars = new List<Jar>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        Vector3 spawnLocation = startLocation.position;
        for (int i = 0; i < numJarsToSpawn; ++i)
        {
            Jar newJar = Instantiate(jarPrefab, spawnLocation, Quaternion.identity);
            createdJars.Add(newJar);
            newJar.OnJarDeathEvent += RespawnJar;

            spawnLocation = new Vector3(spawnLocation.x - spaceBetweenJars, spawnLocation.y, spawnLocation.z);
        }
    }

    private void RespawnJar(Jar jar)
    {
        
    }

    public void OnMove(InputAction.CallbackContext input)
    {
        foreach (Jar jar in createdJars)
        {
            jar.OnMove(input.action);
        }
    }
}
