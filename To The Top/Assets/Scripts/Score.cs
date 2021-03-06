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
    public Text winnerText;
    public Text resultsText;

    int localPlayerNumber;
    public GameObject heightScanner;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //throw new System.NotImplementedException();
        /*
        if (stream.IsWriting)
        {
            stream.SendNext(localPlayerNumber);
            stream.SendNext(scores[localPlayerNumber - 1]);
            stream.SendNext(heights[localPlayerNumber - 1]);
            stream.SendNext(heightsNorm[localPlayerNumber - 1]);
            stream.SendNext(penalties[localPlayerNumber - 1]);

        }
        else
        {
            int receivedPlayerNumber = (int)stream.ReceiveNext();
            this.scores[receivedPlayerNumber - 1] = (int)stream.ReceiveNext();
            this.heights[receivedPlayerNumber - 1] = (float)stream.ReceiveNext();
            this.heightsNorm[receivedPlayerNumber - 1] = (float)stream.ReceiveNext();
            this.penalties[receivedPlayerNumber - 1] = (int)stream.ReceiveNext();

        }
        */

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


        
        Debug.Log("Scores[0]: " + scores[0]); 
        Debug.Log("Scores[1]: " + scores[1]);
        Debug.Log("Scores[2]: " + scores[2]);
        Debug.Log("Scores[3]: " + scores[3]);
        
        this.photonView.RPC("SyncScores", RpcTarget.AllBuffered, localPlayerNumber, scores[localPlayerNumber - 1]);
        StartCoroutine(WaitForScores());

        
    }

    int CalculateScore(int playerNum)
    {
        int tempScore = (10 * (int)heightsNorm[playerNum]) - (5 * penalties[playerNum]);
        return tempScore;
    }

    IEnumerator WaitForScores()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Scores[0]: " + scores[0]);
        Debug.Log("Scores[1]: " + scores[1]);
        Debug.Log("Scores[2]: " + scores[2]);
        Debug.Log("Scores[3]: " + scores[3]);

        resultsText.gameObject.SetActive(true);
        winnerText.gameObject.SetActive(true);

        resultsText.text = "Scores:\n";

        int highscore = scores.Max();
        int winner = scores.ToList().IndexOf(highscore) + 1;

        winnerText.text = "Player " + winner + " wins!\n\n";
        for (int i = 1; i <= PhotonNetwork.CurrentRoom.PlayerCount; i++)
        {
            resultsText.text += "Player " + i + ": " + scores[i - 1] + "\n";
        }
    }

    [PunRPC]
    void SyncScores(int receivedPlayerNum, int score)
    {
        Debug.Log("RPC called, receivedPlayerNum: " + receivedPlayerNum + " score: " + score);
        scores[receivedPlayerNum - 1] = score;
    }
}
