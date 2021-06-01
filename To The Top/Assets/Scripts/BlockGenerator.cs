using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGenerator : MonoBehaviour
{
    List<GameObject> spawnedObjects;
    public GameObject[] objectsToSpawn;

    void Start()
    {
        SpawnObject();
    }

    void Update()
    {
        
    }

    public void SpawnObject()
    {
        spawnedObjects.Add(Instantiate(objectsToSpawn[0], transform.position, transform.rotation, transform));
    }
}
