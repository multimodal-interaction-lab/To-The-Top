using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MultiSceneManager : MonoBehaviour
{
    public string playerPrefabName;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnPlayer", 2f);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void SpawnPlayer()
    { 
        PhotonNetwork.Instantiate(playerPrefabName, new Vector3(0, 0, 0), Quaternion.identity);
    }
}
