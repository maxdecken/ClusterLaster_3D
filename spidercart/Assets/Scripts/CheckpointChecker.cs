using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class CheckpointChecker : MonoBehaviour
{
    private List<CheckpointTrigger> checkPointTriggerList;
    private int nextCheckPointTriggerIndex;
    private CheckpointTrigger lastCheckPoint;
    
    private void Awake()
    {
        Transform checkpoints = transform.Find("Checkpoints");
        Debug.Log("Wie viele Kinder? " + transform.childCount);

        checkPointTriggerList = new List<CheckpointTrigger>();
        
        for (int i = 0; i < transform.childCount; i++)
        {
            CheckpointTrigger checkpointTrigger = transform.GetChild(i).GetComponent<CheckpointTrigger>();

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
        
    }

    public CheckpointTrigger getLastCheckPoint()
    {
        Debug.Log(lastCheckPoint);
        return lastCheckPoint;
    }
    
    
}
