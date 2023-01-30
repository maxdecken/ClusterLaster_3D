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
        //PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.player);
        SceneManager.LoadScene(3);
    }

    #endregion

    #region Public Methods

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
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
                //Set unique name for Kart to compare in Collider
                myPlayerPrefabInstance.name = "Kart_" + PhotonNetwork.LocalPlayer.NickName;
            }
        }
    }
}