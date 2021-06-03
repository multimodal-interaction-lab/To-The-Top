using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public Text heightNormText;
    public Text scoreText;
    public Text penaltyText;
    public Text resultsText;

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

        heightNormText.text = "Height: " + heightsNorm[localPlayerNumber - 1].ToString() + " cm";
        penaltyText.text = "Penalties: " + penalties[localPlayerNumber - 1];
        scoreText.text = "Score: " + scores[localPlayerNumber - 1] + " points";
    }

    // Called when block player spawned falls out of bounds
    public void AddPenalty()
    {
        penalties[localPlayerNumber - 1] += 1;
    }

    public void DisplayResults()
    {
       // heightNormText.gameObject.SetActive(false);
        //scoreText.gameObject.SetActive(false);
        //penaltyText.gameObject.SetActive(false);
        resultsText.gameObject.SetActive(true);

        int highscore = scores.Max();
        int winner = scores.ToList().IndexOf(highscore) + 1;

        resultsText.text = "Player " + winner + " wins!\n\n";
        for (int i = 1; i <= PhotonNetwork.CurrentRoom.PlayerCount; i++)
        {
            resultsText.text += "Player " + i + ": " + scores[i-1] + "\n";
        }
    }

    int CalculateScore(int playerNum)
    {
        int tempScore = (10 * (int)heightsNorm[playerNum]) - penalties[playerNum];
        return tempScore;
    }
}
