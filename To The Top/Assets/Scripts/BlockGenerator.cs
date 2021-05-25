using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGenerator : MonoBehaviour
{
    List<GameObject> spawnedObjects;
    public GameObject[] objectsToSpawn;

    GameObject spawnedBlock;

    void Start()
    {
        //SpawnObject();
    }

    void FixedUpdate()
    {
        
    }

    // Spawn the given block and store a reference to it
    public void SpawnBlock(GameObject blockToSpawn)
    {
        spawnedBlock = Instantiate(blockToSpawn, transform.position, transform.rotation);
    }

    // Tell the referenced block to despawn
    public void DespawnBlock()
    {
        //int i = Random.Range(0, objectsToSpawn.Length);
        spawnedBlock.GetComponent<BuildingBlock>().Despawn();
    }
}
