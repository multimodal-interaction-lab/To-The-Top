using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using UnityEngine.UI;

public class GameManager : MonoBehaviourPunCallbacks, IPunObservable
{
    [Tooltip("The prefab to use for representing the player")]
    public GameObject playerPrefab;
    public GameObject scoreKeeper;
    public GameObject stateManagerPrefab;
    public Text timerText;
    public Text waitText;
    public Text resultsText;
    public Text beginText;
    public Transform table1;
    public Transform table2;
    public Transform table3;
    public Transform table4;

    // How long each game phase will be
    public float waitTime = 30f;
    public float playTime = 30f;
    public float endTime = 30f;

    string homeScene = "TestGameMenu";
    GameObject localPlayer;
    GameObject stateManager;

    float timeRemaining;
    bool timerIsRunning;

    #region Unity Callbacks
    private void Start()
    {
        if (playerPrefab == null)
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
        }
        else
        {
            Debug.LogFormat("We are Instantiating LocalPlayer from {0}", SceneManager.GetActiveScene().name);
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
            stateManager = GameObject.Find("StateManager(Clone)");

            // create state manager if one was not found and this is the master client
            if (stateManager == null && PhotonNetwork.IsMasterClient)
            {
                //stateManager = Instantiate(stateManagerPrefab);
                stateManager = PhotonNetwork.InstantiateRoomObject(stateManagerPrefab.name, new Vector3(0f, 0f, 0f), Quaternion.identity, 0);
                stateManager.SetActive(true);
                stateManager.GetComponent<StateManager>().state = StateManager.States.Waiting;
                DontDestroyOnLoad(stateManager);
            }

            // Check which state to use
            if (stateManager.GetComponent<StateManager>().state == StateManager.States.Waiting)
            {
                this.photonView.RPC("BeginWaitingRPC", RpcTarget.All);
                StartTimer(waitTime);
            }
            else if (stateManager.GetComponent<StateManager>().state == StateManager.States.Playing)
            {
                this.photonView.RPC("BeginPlayingRPC", RpcTarget.All);
                StartTimer(playTime);
            }
        }
    }

    IEnumerator BeginMessageCoroutine()
    {
        beginText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);

        // Fade out
        Color lerpedColor = beginText.color;
        while (beginText.color.a >= 0.01)
        {
            lerpedColor = Color.Lerp(lerpedColor, Color.clear, 0.07f);
            beginText.color = lerpedColor;
            yield return null;
        }

        beginText.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        // Only decrement timer if master client
        if (PhotonNetwork.IsMasterClient)
        {
            if (timerIsRunning)
            {
                if (timeRemaining > 0)
                {
                    timeRemaining -= Time.deltaTime;
                }
                else
                {
                    Debug.Log("Time has run out!");
                    timeRemaining = 0;
                    timerIsRunning = false;

                    // Move to next state
                    if (stateManager.GetComponent<StateManager>().state == StateManager.States.Waiting)
                    {
                        stateManager.GetComponent<StateManager>().state = StateManager.States.Playing;
                        this.photonView.RPC("BeginPlayingRPC", RpcTarget.All);
                        StartTimer(playTime);
                        //Debug.Log("Reloading Level");
                        //PhotonNetwork.LoadLevel("Room for 4");
                    }
                    else if (stateManager.GetComponent<StateManager>().state == StateManager.States.Playing)
                    {
                        stateManager.GetComponent<StateManager>().state = StateManager.States.Ending;

                        this.photonView.RPC("BeginEndingRPC", RpcTarget.All);
                        StartTimer(endTime);
                    }
                    else // in end state going to wait state
                    {
                        stateManager.GetComponent<StateManager>().state = StateManager.States.Waiting;

                        this.photonView.RPC("LeaveRPC", RpcTarget.All);
                        //LeaveRoom();
                    }
                }
            }
        }


        DisplayTime(timeRemaining);
    }
    #endregion

    #region RPCs
    [PunRPC]
    void BeginWaitingRPC()
    {
        waitText.gameObject.SetActive(true);
        resultsText.gameObject.SetActive(false);
    }

    [PunRPC]
    void BeginPlayingRPC()
    {
        waitText.gameObject.SetActive(false);
        resultsText.gameObject.SetActive(false);
        StartCoroutine(BeginMessageCoroutine());
    }

    [PunRPC]
    void BeginEndingRPC()
    {
        resultsText.gameObject.SetActive(true);
    }

    [PunRPC]
    void LeaveRPC()
    {
        LeaveRoom();
    }

    #endregion

    #region Private Methods
    private void StartTimer(float seconds)
    {
        timerIsRunning = true;
        timeRemaining = seconds;
    }
    private void DisplayTime(float timeToDisplay)
    {

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    #endregion

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

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //throw new System.NotImplementedException();

        if (stream.IsWriting)
        {
            stream.SendNext(timeRemaining);
            stream.SendNext(timerIsRunning);
        }
        else
        {
            this.timeRemaining = (float)stream.ReceiveNext();
            this.timerIsRunning = (bool)stream.ReceiveNext();
        }
    }

    #endregion

    #region Public Methods
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    #endregion


}