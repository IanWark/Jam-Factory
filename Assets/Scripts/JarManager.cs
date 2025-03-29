using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JarManager : MonoBehaviour
{
    [SerializeField]
    private int numJarsOnScreen;

    [Header("References")]
    [SerializeField]
    private SpriteRenderer beltSprite;

    [SerializeField]
    private Transform startLocation;

    [SerializeField]
    private Jar jarPrefab;

    private List<Jar> createdJars = new List<Jar>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        float beltSize = beltSprite.sprite.bounds.size.x;
        float spaceBetweenJars = beltSize / numJarsOnScreen;

        Vector3 spawnLocation = startLocation.position;
        for (int i = 0; i < numJarsOnScreen; ++i)
        {
            createdJars.Add(Instantiate(jarPrefab, spawnLocation, Quaternion.identity));

            spawnLocation = new Vector3(spawnLocation.x - spaceBetweenJars, spawnLocation.y, spawnLocation.z);
        }
    }

    public void OnMove(InputAction.CallbackContext input)
    {
        foreach (Jar jar in createdJars)
        {
            jar.OnMove(input.action);
        }
    }
}
