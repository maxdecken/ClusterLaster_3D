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
    public GameObject playerPrefab_default;
    public GameObject playerPrefab_evil;

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
        if (playerPrefab_default == null || playerPrefab_evil == null) {
            Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Pun Game Manager'",this);
        }
        else {
            if (PlayerController.LocalPlayerInstance == null) {

                GameStateController gameStateController = GameObject.FindObjectOfType<GameStateController>();
                PlayerDataContainer playerDataContainer = (PlayerDataContainer) GameObject.FindObjectOfType(typeof(PlayerDataContainer));

                if(playerDataContainer == null){
                    Debug.Log("No PlayerDataContainer found: Creating new one!");
                    // New playerDataContainer with default settings
                    GameObject playerDataContainerGameObject = new GameObject("PlayerDataContainer");
                    playerDataContainerGameObject.gameObject.AddComponent<PlayerDataContainer>();
                    playerDataContainerGameObject.AddComponent<DontDestroy>();

                    playerDataContainer = playerDataContainerGameObject.GetComponent<PlayerDataContainer>();
                }

                GameObject selectedPlayerPrefab = playerPrefab_default;
                if(playerDataContainer.playerCharacterType == PlayerDataContainer.CharacterType.SpiderPiggyEvil){
                    selectedPlayerPrefab = playerPrefab_evil;
                }

                // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
                GameObject myPlayerPrefabInstance = PhotonNetwork.Instantiate(
                    selectedPlayerPrefab.name,
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
                //myPlayerPrefabInstance.GetComponent<PlayerController>().SetKartAndCharacter(playerDataContainer);
            }
        }
    }
}