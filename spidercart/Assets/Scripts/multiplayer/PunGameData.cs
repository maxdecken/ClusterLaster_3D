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

    public void PlayerFinishedRace(){
        NumberPlayersFinished++;
    }

    public int GetNumberPlayersFinished(){
        return NumberPlayersFinished;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting) {
            Debug.Log("IsWriting");
            stream.SendNext(NumberPlayersFinished);
        }
        else {
            Debug.Log("IsNOTWriting");
            NumberPlayersFinished = (int)stream.ReceiveNext();
        }
    }
}
