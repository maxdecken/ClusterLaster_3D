using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using UnityEngine.UI;
using TMPro;

public class PunGameData : MonoBehaviourPunCallbacks, IPunObservable
{
    public static PunGameData Instance;

    public int NumberPlayersFinished = 0;

    public string testMsg;

    public TMP_Text testMsgObject;

    void Start() {
        Instance = this;
        //testMsg = "random: " + Random.Range(-20.0f, 30.0f);
        //testMsgObject.text = testMsg;
    }

    public void PlayerFinishedRace() {
        NumberPlayersFinished++;
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("Rpc_PlayerFinishedRace", RpcTarget.All);
    }

    public int GetNumberPlayersFinished() {
        return NumberPlayersFinished;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting) {
            //Debug.Log("IsWriting " + NumberPlayersFinished);
            stream.SendNext(NumberPlayersFinished);
        }
        else {
            NumberPlayersFinished = (int)stream.ReceiveNext();
            //Debug.Log("IsNOTWriting " + NumberPlayersFinished);
        }
    }

    [PunRPC]
    void Rpc_PlayerFinishedRace() {
        NumberPlayersFinished++;
    }
}
