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
    public GameObject stateManager;
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

            if (PhotonNetwork.IsMasterClient)
            {
                StartTimer(waitTime);
            }

            // Start in wait mode
            waitText.gameObject.SetActive(true);
            stateManager.GetComponent<StateManager>().state = StateManager.States.Waiting;
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
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;

                // Can put stuff here to happen at 10 seconds left etc...
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
                    waitText.gameObject.SetActive(false);
                    resultsText.gameObject.SetActive(false);
                    StartCoroutine(BeginMessageCoroutine());
                    ResetScene();
                    StartTimer(playTime);
                }
                else if (stateManager.GetComponent<StateManager>().state == StateManager.States.Playing)
                {
                    stateManager.GetComponent<StateManager>().state = StateManager.States.Ending;
                    resultsText.gameObject.SetActive(true);
                    StartTimer(endTime);
                }
                else // in end state going to wait state
                {
                    stateManager.GetComponent<StateManager>().state = StateManager.States.Waiting;
                    LeaveRoom();
                }
            }

        }


        DisplayTime(timeRemaining);
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

    private void ResetScene()
    {
        // Find and despawn all building blocks
        BuildingBlock[] buildingBlocks = FindObjectsOfType<BuildingBlock>();
        foreach(BuildingBlock block in buildingBlocks)
        {
            block.Despawn();
        }

        // Clear penalties
        scoreKeeper.GetComponent<Score>().penalties = new int[4];
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