using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class PunLauncher : MonoBehaviourPunCallbacks
{

    #region Private Serializable Fields

    [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
    [SerializeField]
    private byte maxPlayersPerRoom = 4;

    [Tooltip("The UI Label to inform the user that the connection is in progress")]
    [SerializeField]
    private GameObject progressLabel;

    [SerializeField]
    private GameObject roomPlayersLabel;

    [SerializeField]
    private GameObject startButton;


    #endregion

    #region Private Fields

    /// &lt;summary&gt;
    /// This client's version number. Users are separated from each other by gameVersion (which allows you to make breaking changes).
    /// &lt;/summary&gt;
    string gameVersion = "1";

    #endregion

    #region MonoBehaviour CallBacks

    /// &lt;summary&gt;
    /// MonoBehaviour method called on GameObject by Unity during early initialization phase.
    /// &lt;/summary&gt;
    void Awake()
    {
        // #Critical
        // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    /// &lt;summary&gt;
    /// MonoBehaviour method called on GameObject by Unity during initialization phase.
    /// &lt;/summary&gt;
    void Start()
    {
        startButton.SetActive(false);
        Connect();
    }

    #endregion


    #region Public Methods

    /// &lt;summary&gt;
    /// Start the connection process.
    /// - If already connected, we attempt joining a random room
    /// - if not yet connected, Connect this application instance to Photon Cloud Network
    /// &lt;/summary&gt;
    public void Connect()
    {
        // we check if we are connected or not, we join if we are , else we initiate the connection to the server.
        if (PhotonNetwork.IsConnected)
        {
            // #Critical we need at this point to attempt joining a Random Room. If it fails, we'll get notified in OnJoinRandomFailed() and we'll create one.
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            // #Critical, we must first and foremost connect to Photon Online Server.
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }
    }

    #endregion

    #region MonoBehaviourPunCallbacks Callbacks

    public override void OnConnectedToMaster()
    {
        Debug.Log("PUN: OnConnectedToMaster() was called by PUN");
        progressLabel.GetComponent<TMP_Text>().text += "\nconnected to server";
        PhotonNetwork.JoinRandomRoom(); // If this fails the onJoinRoomFailed Method is called to create one
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("PUN: OnDisconnected() was called by PUN with reason {0}", cause);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("PUN: OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");

        // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });

        // User is room creater so only alow him to start the game
        startButton.SetActive(true);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("PUN: OnJoinedRoom() called by PUN. Now this client is in a room.");
        progressLabel.GetComponent<TMP_Text>().text += "\njoined room";
        updateRoomPlayerLabel();
    }

    public override void OnLeftRoom()
    {
        Debug.Log("PUN: OnJoinedRoom() called by PUN. Now this client is in a room.");
        progressLabel.GetComponent<TMP_Text>().text += "\\nleft room";
    }

    public override void OnPlayerEnteredRoom(Player other)
    {
        Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName); // not seen if you're the player connecting
        progressLabel.GetComponent<TMP_Text>().text += "\nPlayer joined: " + other.NickName;
        updateRoomPlayerLabel();

        if (PhotonNetwork.IsMasterClient) {
            Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom
        }
    }

    public override void OnPlayerLeftRoom(Player other)
    {
        Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName); // seen when other disconnects
        progressLabel.GetComponent<TMP_Text>().text += "\nPlayer left: " + other.NickName;
        updateRoomPlayerLabel();

        if (PhotonNetwork.IsMasterClient) {
            Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom
        }
    }

    private void updateRoomPlayerLabel () {
        string playListString = "Players in Room: ";
        Dictionary<int, Photon.Realtime.Player> pList = PhotonNetwork.CurrentRoom.Players;
        foreach (KeyValuePair<int, Photon.Realtime.Player> p in pList) {
            playListString += "\n" + p.Value.NickName;
        }
        roomPlayersLabel.GetComponent<TMP_Text>().text = playListString;
    }

    #endregion


    public void goToMainScene () {
        if (PhotonNetwork.CurrentRoom != null) {
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }
        SceneManager.LoadScene("MainGame");
    }

    public void goBack () {
        // First disable game manager to prevent player from being redirected to end scene
        GameObject.Find("MultiplayerGameManager").SetActive(false);
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("StartScene");
    }
}
