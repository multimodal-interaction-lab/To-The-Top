using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;

public class LobbyManager : MonoBehaviour
{

    string gameVersion = "1";
    [SerializeField]
    GameObject buttonPanel;
    [SerializeField]
    Text ConnectionStatusText;
    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        //buttonPanel.SetActive(false);
        Connect();
    }

    // Update is called once per frame
    void Update()
    {
        ConnectionStatusText.text = "Connection Status: "+ PhotonNetwork.NetworkClientState;
    }

    void Connect()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }
    }

    public void OnConnectedToMaster()
    {
        buttonPanel.SetActive(true);
    }

    public void JoinGamePressed()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public void CreateGamePressed()
    {
        
        if (PhotonNetwork.IsConnected)
        {
            string roomName = "Room " + Random.Range(1000, 10000);
            RoomOptions options = new RoomOptions { MaxPlayers = 2, PlayerTtl = 10000 };
            PhotonNetwork.CreateRoom(roomName, options, null);

            PhotonNetwork.LoadLevel("MultiplayerTest-GameScene");
        }

        
    }
}
