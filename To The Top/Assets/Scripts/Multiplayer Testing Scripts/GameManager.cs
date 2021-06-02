using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;



public class GameManager : MonoBehaviourPunCallbacks
{
    [Tooltip("The prefab to use for representing the player")]
    public GameObject playerPrefab;
    public Transform table1;
    public Transform table2;
    public Transform table3;
    public Transform table4;
    string homeScene = "TestGameMenu";

    GameObject localPlayer;
    public GameObject scoreKeeper;

    private void Start()
    {
        if (playerPrefab == null)
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
        }
        else
        {
            Debug.LogFormat("We are Instantiating LocalPlayer from {0}", Application.loadedLevelName);
            PhotonNetwork.NickName = "Player " + PhotonNetwork.LocalPlayer.ActorNumber;
            
            if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
            {
                localPlayer = Instantiate(this.playerPrefab, table1.position, table1.rotation);
            }
            else if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
            {
                localPlayer = Instantiate(this.playerPrefab, table2.position, table2.rotation);
            }
            else if (PhotonNetwork.LocalPlayer.ActorNumber == 3)
            {
                localPlayer = Instantiate(this.playerPrefab, table3.position, table3.rotation);
            }
            else if (PhotonNetwork.LocalPlayer.ActorNumber == 4)
            {
                localPlayer = Instantiate(this.playerPrefab, table4.position, table4.rotation);
            }

            scoreKeeper.GetComponent<Score>().heightScanner = localPlayer.transform.Find("HeightScanner").gameObject;
        }
    }

    #region Photon Callbacks

    public override void OnPlayerEnteredRoom(Player other)
    {
        Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName); // not seen if you're the player connecting
        Debug.Log("Player number: " + other.ActorNumber);

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom
        }
    }


    public override void OnPlayerLeftRoom(Player other)
    {
        Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName); // seen when other disconnects


        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom
        }
    }
    /// <summary>
    /// Called when the local player left the room. We need to load the launcher scene.
    /// </summary>
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(homeScene);
    }
    #endregion


    #region Public Methods


    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }


    #endregion

    #region Private Methods
    


    #endregion
}
