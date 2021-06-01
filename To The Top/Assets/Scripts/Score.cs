using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Score : MonoBehaviourPun, IPunObservable
{
    // Player scores are indexed at their  PhotonNetwork.LocalPlayer.ActorNumber - 1;
    public float[] scores;
    public float[] heights;
    public float[] penalties;

    public Text playerNameText;
    public Text heightText;
    int localPlayerNumber;
    public GameObject heightScanner;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //throw new System.NotImplementedException();

        if (stream.IsWriting)
        {
            stream.SendNext(scores);
            stream.SendNext(heights);
            stream.SendNext(penalties);
        }
        else
        {
            scores = (float[])stream.ReceiveNext();
            heights = (float[])stream.ReceiveNext();
            penalties = (float[])stream.ReceiveNext();
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        localPlayerNumber = PhotonNetwork.LocalPlayer.ActorNumber;
        scores = new float[4];
        heights = new float[4];
        penalties = new float[4];
        playerNameText.text = "Player " + localPlayerNumber;
        heightText.text = "Height: 0";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        heights[localPlayerNumber - 1] = heightScanner.GetComponent<HeightTriggerBehavior>().readTowerHeight();
        heightText.text = "Height: " + heights[localPlayerNumber - 1].ToString();
    }

}
