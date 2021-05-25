using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGenerator : MonoBehaviour
{
    List<GameObject> spawnedObjects;
    public GameObject[] objectsToSpawn;

    void Start()
    {
        //SpawnObject();
    }

    void FixedUpdate()
    {
        
    }

    public void SpawnObject(GameObject objectToSpawn)
    {
        //int i = Random.Range(0, objectsToSpawn.Length);
        spawnedObjects.Add(Instantiate(objectToSpawn, transform.position, transform.rotation));
    }
}
