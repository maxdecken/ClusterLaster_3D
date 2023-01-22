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

    public string testMsg;

    public TMP_Text testMsgObject;

    void Start() {
        Instance = this;
        testMsg = "random: " + Random.Range(-20.0f, 30.0f);
        testMsgObject.text = testMsg;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        Debug.Log("something");
        if (stream.IsWriting) {
            // We own this player: send the others our data
            Debug.Log("Sending: Some Data");
            stream.SendNext(testMsg);
        }
        else {
            // Network player, receive data
            Debug.Log("Received Data: " + stream.ReceiveNext());
        }
    }
}
