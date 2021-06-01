using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

/*
 * Attach this script to a despawn plane which should have a collider with trigger enabled and a rigidbody that is kinematic.
 * The despawn plane should be invisible and have a parent, child, or sibling that is a visible plane with a mesh collider to stop the object
 */

public class Boundary : MonoBehaviourPun
{

    GameObject ScoreKeeper;

    // Start is called before the first frame update
    void Start()
    {
        ScoreKeeper = GameObject.Find("ScoreKeeper");
    }

    // Update is called once per frame
    void Update()
    {

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
                ScoreKeeper.GetComponent<Score>().AddPenalty(((BuildingBlock)component).playerNum);
            }

            ((BuildingBlock)component).Despawn();
        }
    }
}
