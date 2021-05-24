﻿using System.Collections;
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

    void FixedUpdate()
    {
        
    }

    public void SpawnObject()
    {
        spawnedObjects.Add(Instantiate(objectsToSpawn[1], transform.position, transform.rotation, transform));
    }
}
