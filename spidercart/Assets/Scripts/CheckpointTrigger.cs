using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    private CheckpointChecker checkpointChecker;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            checkpointChecker.AlertCheckpointTrigger(this);

        }
    }

    public void SetCheckpoints(CheckpointChecker checkpointChecker)
    {
         this.checkpointChecker = checkpointChecker;
    }
}
