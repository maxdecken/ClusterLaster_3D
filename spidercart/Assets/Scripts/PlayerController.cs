using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

using Photon.Pun;
using Photon.Realtime;
using Cinemachine;

public class PlayerController : MonoBehaviourPunCallbacks, IPunObservable
{
    [Header("Driving-Behavoir")]
    [SerializeField] private float velocity = 200000;
    [SerializeField] private float maxVelocity = 15;
    [SerializeField] private float dragStrength = 0.99f;
    [SerializeField] private float steeringStrength = 60;
    [SerializeField] private float maxAngularVelocity = 10;

    [Header("References")]
    private float vehicleSize;
    [SerializeField] private Animator playerAnimations = null;
    [SerializeField] private GameStateController gameStateController = null;
    [SerializeField] PlayerController checkpointChecker;
    [SerializeField] GameObject StartingPosition;
    [SerializeField] GameObject cameraLookAt;
    private float RayDistance = 3;
    private Rigidbody rigidbody;
    private bool isOnTop;
    private bool isOnSide;
    private LayerMask groundLayer;
    private float timer;
    private int place;
    private PunGameData punGameData = null;

    private GameObject checkpointContainer;
    private List<GameObject> checkPointTriggerList;
    private int nextCheckPointTriggerIndex;
    private GameObject lastCheckPoint;

    //Use to disbale controlls before start and after race has finished, is used in GameStateController
    public bool controllsAllowed = false;
    public bool respawnCooldown = false;
    public bool raceFinished = false;
    private bool startRaceStateSet = false;
    private bool isSlipping = false;
    private float saveSteeringStrength;
    private float currentRotation;
    private float steepPush;

    //Binding if using On-Screencontrolls (Main Method)
    public InputAction joystick = new InputAction("look", binding: "<Gamepad>/leftStick");
    

    private Vector3 Movement;

    // Multiplayer
    [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
    public static GameObject LocalPlayerInstance;
    
    
    private void Awake()
    {
        gameStateController = GameObject.FindObjectOfType<GameStateController>();
        punGameData = GameObject.FindObjectOfType<PunGameData>();
        checkpointContainer = GameObject.FindGameObjectWithTag("Container");
        Transform checkpoints = checkpointContainer.transform.Find("Checkpoints");

        checkPointTriggerList = new List<GameObject>();
        
        for (int i = 0; i < checkpointContainer.transform.childCount; i++)
        {
            GameObject checkpointTrigger = checkpointContainer.transform.GetChild(i).gameObject;
            
            checkPointTriggerList.Add(checkpointTrigger);
        }
    }

    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        //Set max angular velocity for turning
        rigidbody.maxAngularVelocity = maxAngularVelocity;

        vehicleSize = GetComponent<BoxCollider>().bounds.extents.y;

        //Binding if using Keyboard/ Editor
        joystick.AddCompositeBinding("Dpad")
            .With("Up", "<Keyboard>/w")
            .With("Down", "<Keyboard>/s")
            .With("Left", "<Keyboard>/a")
            .With("Right", "<Keyboard>/d");

        joystick.Enable();
        //gamepad = Gamepad.current;
        checkpointChecker = this.transform.GetComponent<PlayerController>();
        if (photonView.IsMine) {
            PlayerController.LocalPlayerInstance = this.gameObject;
            controllsAllowed = false;

            gameStateController.SetPlayerController(this);
            
            StartingPosition =  GameObject.Find("StartingPosition");

            // Attach Cinematic Camera
            GameObject cm = GameObject.Find("CMFreeLook1");
            cm.GetComponent<CinemachineFreeLook>().Follow = this.transform;
            cm.GetComponent<CinemachineFreeLook>().LookAt = cameraLookAt.transform;
        }
        
    }
    
    private void FixedUpdate()
    {
        currentRotation = transform.rotation.y;
        currentRotation += (Mathf.Sin(Time.deltaTime/ 1));
        if (isSlipping)
        {
            transform.Rotate(transform.rotation.x, 440f * Time.deltaTime, transform.rotation.z);
            
        }
        
        // is on top check
        isOnTop = Physics.Raycast(transform.position, Vector3.up, RayDistance);
        if (transform.rotation.z > 70 || transform.rotation.z < -70)
        {
            isOnSide = true;
        }
        else
        {
            isOnSide = false;
        }

        bool isOnGround = Physics.Raycast(transform.position, Vector3.up, 2.15f);
        //Debug.Log("isOnGround: " + isOnGround);
        
        if (isOnTop || isOnSide)
        {
            timer = 0.0f;
            //return;
        }

        timer += Time.deltaTime;
        if (timer > 3.3f)
        {
            Debug.Log("RESPAWN!");
            respawn();

            timer = timer - 3.3f; // reset timer
        }
        
        if(controllsAllowed && !(respawnCooldown)){
            if(!startRaceStateSet){
                playerAnimations.SetBool("startRace", true);
            }
            if(isOnGround){
                // Controlls are based on/ inspired by the Unity-Karting Sample
                float currentSpeed = rigidbody.velocity.magnitude;

                // apply inputs to forward/backward
                float turnAmmount = joystick.ReadValue<Vector2>().x;
                float turningPower = turnAmmount * steeringStrength;

                if (transform.rotation.x < -20)
                {
                    steepPush = 1.3f;
                }
                else
                {
                    steepPush = 1f;
                }

                Quaternion turnAngle = Quaternion.AngleAxis(turningPower, transform.up);
                Vector3 fwd = turnAngle * transform.forward;
                Vector3 movement = fwd * velocity * joystick.ReadValue<Vector2>().y;

                // forward movement
                bool wasOverMaxSpeed = currentSpeed >= maxVelocity;

                // if over max speed, cannot accelerate faster.
                if (wasOverMaxSpeed) 
                    movement *= 1.0f;

                Vector3 newVelocity = rigidbody.velocity + movement * steepPush * Time.fixedDeltaTime;
                newVelocity.y = rigidbody.velocity.y;
                
                rigidbody.velocity = newVelocity;

                var angularVel = rigidbody.angularVelocity;
        
                // move the Y angular velocity towards our target
                angularVel.y = Mathf.MoveTowards(angularVel.y, turningPower * 0.4f, Time.fixedDeltaTime * 20f);

                // apply the angular velocity
                rigidbody.angularVelocity = angularVel;
            
                Vector3 localVel = transform.InverseTransformVector(rigidbody.velocity);
                rigidbody.velocity = Quaternion.AngleAxis(turningPower * Mathf.Sign(localVel.z) * steeringStrength * Time.fixedDeltaTime, transform.up) * rigidbody.velocity;
                
                //In one of 10000 Tests the piggy should hit the HonkingHorn randomly
                int randomHonkingHorn = Random.Range(0, 10001);
                if(randomHonkingHorn == 10000){
                    playerAnimations.SetTrigger("honkingHorn");
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!controllsAllowed){
            if(gameStateController.IsRaceStarted()){
                controllsAllowed = true;
            }
        }

        SetPlayerPlace();

        if(raceFinished){
            if(place <= 1){
                playerAnimations.SetBool("raceWon", true);
            }else{
                playerAnimations.SetBool("raceLost", true);
            }
        }
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SeagullDropping"))
        {
            SlipUp();

        }
        
        if (other.gameObject.CompareTag("FallBound"))
        {
            respawn();
        }
        if (other.gameObject.CompareTag("CheckpointTrigger"))
        {
            checkpointChecker.AlertCheckpointTrigger(other.gameObject);
        }
    }

    public void respawn()
    {
        StartCoroutine(allowControlsAfterSeconds());
        if (checkpointChecker == null) {
            Debug.Log("ATTENTION CHECKER NOT SET");
            return;
        }

        rigidbody.velocity = new Vector3(0,0,0);
        rigidbody.angularVelocity = new Vector3(0,0,0);
        if (checkpointChecker.getLastCheckPoint() != null)
        {
            //transform.rotation = new Quaternion(0, 228, 0, 0);
            transform.rotation = checkpointChecker.getLastCheckPoint().transform.rotation;
            transform.position = checkpointChecker.getLastCheckPoint().transform.position;
            rigidbody.velocity = new Vector3(0,0,0);
            rigidbody.angularVelocity = new Vector3(0,0,0);
        }
        else
        {
            Debug.Log("No checkpoint!");
            transform.rotation = StartingPosition.transform.rotation;
            transform.position = StartingPosition.transform.position;
            rigidbody.velocity = new Vector3(0,0,0);
            rigidbody.angularVelocity = new Vector3(0,0,0);
        }
    }
    public void SlipUp()
    {
        StartCoroutine(SlipUpCoroutine());
    }
    
    public IEnumerator SlipUpCoroutine()
    {
        Debug.Log("Hit");
        //saveSteeringStrength = steeringStrength;
        isSlipping = true;
        //steeringStrength = 0;
        yield return new WaitForSeconds(1.3f);
        isSlipping = false;
        //steeringStrength = saveSteeringStrength;
    }

    public IEnumerator allowControlsAfterSeconds()
    {
        respawnCooldown = true;
        yield return new WaitForSeconds(0.8f);
        respawnCooldown = false;
        //steeringStrength = saveSteeringStrength;
    }

    /****
    ************* MULTIPLAYER STUFF ***************
    **/

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting) {
            // We own this player: send the others our data
            //Debug.Log("Sending: Some Data");
            stream.SendNext(transform.position);
        }
        else {
            // Network player, receive data
            //Debug.Log("Received Data: " + stream.ReceiveNext());
        }
    }
    
    public void AlertCheckpointTrigger(GameObject checkpointTrigger)
    {
        if (checkPointTriggerList.IndexOf(checkpointTrigger) == nextCheckPointTriggerIndex)
        {
            setRespawnPosition();
            nextCheckPointTriggerIndex++;
        }
    }

    public void setRespawnPosition()
    {
        lastCheckPoint = checkPointTriggerList[nextCheckPointTriggerIndex];

    }

    public GameObject getLastCheckPoint()
    {
        return lastCheckPoint;
    }

    public void SetPlayerPlace()
    {   
        if(!raceFinished){
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            int numInFront = 0;
            //int currentPlayerCheckpointIndex = checkPointTriggerList.IndexOf(lastCheckPoint);
            int currentPlayerCheckpointIndex = nextCheckPointTriggerIndex;
            //Debug.Log("currentPlayerCheckpointIndex: " + currentPlayerCheckpointIndex);
            //Debug.Log("Dist next Checkpoint: " + Vector3.Distance(this.transform.position, checkPointTriggerList[nextCheckPointTriggerIndex].transform.position).ToString());
            foreach(GameObject player in players){
                PlayerController otherPlayerPlayerController = player.GetComponent<PlayerController>();

                if(!otherPlayerPlayerController.raceFinished){
                    int otherPlayerCheckpointIndex = otherPlayerPlayerController.nextCheckPointTriggerIndex;
                    if(otherPlayerCheckpointIndex > currentPlayerCheckpointIndex){
                        //If that is the case the other player is already at the next checktpoint and so infront
                        numInFront ++;
                    }else if(otherPlayerCheckpointIndex == currentPlayerCheckpointIndex){
                        //Clac if other is in front
                        GameObject nextCheckPoint = checkPointTriggerList[nextCheckPointTriggerIndex];
                        float distCurrentPlayer = Vector3.Distance(this.transform.position, nextCheckPoint.transform.position);
                        float distOtherPlayer = Vector3.Distance(player.transform.position, nextCheckPoint.transform.position);
                        if(distOtherPlayer >= distCurrentPlayer){
                            numInFront++;
                        }
                    }
                }
            }
            gameStateController.place = numInFront + punGameData.GetNumberPlayersFinished();
            gameStateController.SetPlaceText();
        }
    }
}
