using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [SerializeField] private PlayerController player = null;
    [SerializeField] private GameStateController gameStateController = null;
    [SerializeField] private AudioSource FinishSound;

    void Start(){
        
    }
    
    private void OnTriggerEnter(Collider collider){
        Debug.Log(collider.gameObject.name);
        if(player == null){
            player = gameStateController.player;
        }
        Debug.Log(player.gameObject.name);
        if(collider.gameObject.name == player.gameObject.name){
            Debug.Log("Race Finished!!!");
            gameStateController.RaceFinished();
            FinishSound.Play();
        }
    }
}
