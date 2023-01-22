using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class CheckpointChecker : MonoBehaviour
{
    private GameObject checkpointContainer;
    private List<CheckpointTrigger> checkPointTriggerList;
    private int nextCheckPointTriggerIndex;
    private CheckpointTrigger lastCheckPoint;
    
    private void Awake()
    {
        checkpointContainer = GameObject.FindGameObjectWithTag("Container");
        Transform checkpoints = checkpointContainer.transform.Find("Checkpoints");
        Debug.Log("Wie viele Kinder? " + checkpointContainer.transform.childCount);

        checkPointTriggerList = new List<CheckpointTrigger>();
        
        for (int i = 0; i < checkpointContainer.transform.childCount; i++)
        {
            CheckpointTrigger checkpointTrigger = checkpointContainer.transform.GetChild(i).GetComponent<CheckpointTrigger>();

            checkpointTrigger.SetCheckpoints(this);
            checkPointTriggerList.Add(checkpointTrigger);
        }
        
    }
    
    
    

    public void AlertCheckpointTrigger(CheckpointTrigger checkpointTrigger)
    {
        if (checkPointTriggerList.IndexOf(checkpointTrigger) == nextCheckPointTriggerIndex)
        {
            Debug.Log("correct");
            setRespawnPosition();
            nextCheckPointTriggerIndex++;
            
        }
        else
        {
            Debug.Log("wrong");
        }
    }

    public void setRespawnPosition()
    {
        lastCheckPoint = checkPointTriggerList[nextCheckPointTriggerIndex];
        Debug.Log("Last Ceckpoint: " + lastCheckPoint);
        
    }

    public CheckpointTrigger getLastCheckPoint()
    {
        Debug.Log(lastCheckPoint);
        return lastCheckPoint;
    }
    
    
}
