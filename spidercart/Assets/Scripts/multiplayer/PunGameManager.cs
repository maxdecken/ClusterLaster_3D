using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

using Photon.Pun;
using Photon.Realtime;

public class PunGameManager : MonoBehaviourPunCallbacks
{

    [Tooltip("The UI Label to inform the user that the connection is in progress")]
    [SerializeField]
    private GameObject progressLabel;

    [Tooltip("The prefab to use for representing the player")]
    public GameObject playerPrefab;

    public static PunGameManager Instance;

    void Start() {
        Instance = this;
    }

    #region Photon Callbacks

    /// &lt;summary&gt;
    /// Called when the local player left the room. We need to load the launcher scene.
    /// &lt;/summary&gt;
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }

    #endregion

    #region Public Methods

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnPlayerEnteredRoom(Player other)
    {
        Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName); // not seen if you're the player connecting
        progressLabel.GetComponent<TMP_Text>().text += "\nPlayer joined: " + other.NickName;

        if (PhotonNetwork.IsMasterClient) {
            Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom
        }
    }

    public override void OnPlayerLeftRoom(Player other)
    {
        Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName); // seen when other disconnects

        if (PhotonNetwork.IsMasterClient) {
            Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom
        }
    }

    #endregion

    /*****
    *** ACTUAL GAMEPLAY ETC
    **********/
    public void initiatePlayers () {
        if (playerPrefab == null) {
            Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Pun Game Manager'",this);
        }
        else {
            if (PlayerController.LocalPlayerInstance == null) {
                // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
                PhotonNetwork.Instantiate(
                    this.playerPrefab.name,
                    new Vector3(
                        -151.81f,
                        64.14f,
                        4 + PhotonNetwork.LocalPlayer.ActorNumber + 1
                    ),
                    new Quaternion(
                        0,
                        1,
                        0,
                        1
                    ),
                    0
                );
            }
        }
    }
}