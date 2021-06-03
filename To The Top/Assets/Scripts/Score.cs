﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Score : MonoBehaviourPun, IPunObservable
{
    // Player scores are indexed at their  PhotonNetwork.LocalPlayer.ActorNumber - 1;
    public int[] scores;
    public float[] heights;
    public float[] heightsNorm;
    public int[] penalties;

    public Text playerNameText;
    public Text heightText;
    public Text heightNormText;
    public Text scoreText;
    public Text penaltyText;

    int localPlayerNumber;
    public GameObject heightScanner;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //throw new System.NotImplementedException();

        if (stream.IsWriting)
        {
            stream.SendNext(scores);
            stream.SendNext(heights);
            stream.SendNext(heightsNorm);
            stream.SendNext(penalties);
        }
        else
        {
            this.scores = (int[])stream.ReceiveNext();
            this.heights = (float[])stream.ReceiveNext();
            this.heightsNorm = (float[])stream.ReceiveNext();
            this.penalties = (int[])stream.ReceiveNext();
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        localPlayerNumber = PhotonNetwork.LocalPlayer.ActorNumber;

        scores = new int[4];
        heights = new float[4];
        heightsNorm = new float[4];
        penalties = new int[4];

        playerNameText.text = "Player " + localPlayerNumber;
        heightText.text = "Height (raw): 0";
        heightNormText.text = "Height: 0 cm";
        scoreText.text = "Score: 0 points";
        penaltyText.text = "Penalties: 0";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        heights[localPlayerNumber - 1] = heightScanner.GetComponent<HeightTriggerBehavior>().readTowerHeight();
        heightsNorm[localPlayerNumber - 1] = heightScanner.GetComponent<HeightTriggerBehavior>().readTowerHeightNorm();
        scores[localPlayerNumber - 1] = CalculateScore(localPlayerNumber - 1);

        heightText.text = "Height (raw): " + heights[localPlayerNumber - 1].ToString();
        heightNormText.text = "Height: " + heightsNorm[localPlayerNumber - 1].ToString() + " cm";
        penaltyText.text = "Penalties: " + penalties[localPlayerNumber - 1];
        scoreText.text = "Score: " + scores[localPlayerNumber - 1] + " points";
    }

    // Called when block player spawned falls out of bounds
    public void AddPenalty()
    {
        penalties[localPlayerNumber - 1] += 1;
    }

    int CalculateScore(int playerNum)
    {
        int tempScore = (10 * (int)heightsNorm[playerNum]) - penalties[playerNum];
        return tempScore;
    }
}
