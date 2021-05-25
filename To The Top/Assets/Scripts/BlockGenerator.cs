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
    }

    void FixedUpdate()
    {
        
    }

    // Spawn the given block and store a reference to it
    public void SpawnBlock(GameObject blockToSpawn)
    {
        Debug.Log("SpawnBlock called");
        spawnedBlock = Instantiate(blockToSpawn, transform.position, transform.rotation);
    }

    // Tell the referenced block to despawn
    public void DespawnBlock()
    {
        Debug.Log("DespawnBlock called");
        if (spawnedBlock != null)
        {
            spawnedBlock.GetComponent<BuildingBlock>().Despawn();
            spawnedBlock = null;
        }
    }

    public void ReplaceBlock(GameObject blockToSpawn)
    {
        StartCoroutine(ReplaceBlockCoroutine(blockToSpawn));
    }
    IEnumerator ReplaceBlockCoroutine(GameObject blockToSpawn)
    {
        DespawnBlock();
       
        yield return new WaitForSeconds(0.5f);

        SpawnBlock(blockToSpawn);
    }
}
