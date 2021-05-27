using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class BlockGenerator : MonoBehaviourPun
{
    public GameObject spawnMenuObject;
    public GameObject spawnedBlock;

    // Spawn the given block and store a reference to it
    public void SpawnBlock(GameObject blockToSpawn)
    {
        if (PhotonNetwork.IsConnected == true)
        {
            spawnedBlock = PhotonNetwork.Instantiate(blockToSpawn.name, transform.position, transform.rotation);
        }
        else
        {
            spawnedBlock = Instantiate(blockToSpawn, transform.position, transform.rotation);
        }

        spawnedBlock.GetComponent<BuildingBlock>().spawnMenuObject = spawnMenuObject;
        spawnedBlock.GetComponent<BuildingBlock>().spawnPointObject = gameObject;
    }

    // Tell the referenced block to despawn
    public void DespawnBlock()
    {
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
