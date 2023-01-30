using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class PunLobbyData : MonoBehaviourPunCallbacks, IPunObservable
{
     public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        Debug.Log("something");
        if (stream.IsWriting) {
            // We own this player: send the others our data
            Debug.Log("Sending: Some Data");
            stream.SendNext("testData");
        }
        else {
            // Network player, receive data
            Debug.Log("Received Data: " + stream.ReceiveNext());
        }
    }
}
