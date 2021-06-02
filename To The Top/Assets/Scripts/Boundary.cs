using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Boundary : MonoBehaviourPun
{

    GameObject ScoreKeeper;

    // Start is called before the first frame update
    void Start()
    {
        ScoreKeeper = GameObject.Find("ScoreKeeper");
    }

    // Called when a collision is detected.
    private void OnTriggerEnter(Collider other)
    {
        // if the colliding component is attached to a block with a BuildingBlock script its Despawn function is called
        if (other.gameObject.TryGetComponent(typeof(BuildingBlock), out Component component))
        {
            if (PhotonNetwork.IsConnected == true && ((BuildingBlock)component).playerNum == PhotonNetwork.LocalPlayer.ActorNumber)
            {
                Debug.Log("Penalty for player " + PhotonNetwork.LocalPlayer.ActorNumber + " due to block belonging to " + ((BuildingBlock)component).playerNum);
                ScoreKeeper.GetComponent<Score>().AddPenalty();
            }

            ((BuildingBlock)component).Despawn();
        }
    }
}
