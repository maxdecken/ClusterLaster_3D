using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float velocity = 50;
    [SerializeField] private float maxVelocity = 15;
    [SerializeField] private float dragStrength = 0.99f;
    [SerializeField] private float steeringStrength = 20;
    [SerializeField] private Animator playerAnimations = null;
    [SerializeField] CheckpointChecker checkpointChecker;
    [SerializeField] GameObject StartingPosition;
    [SerializeField] float RayDistance;
    private Rigidbody rigidbody;
    private bool isOnTop;
    private LayerMask groundLayer;
    private float timer;


    //Use to disbale controlls before start and after race has finished, is used in GameStateController
    public bool controllsAllowed = false;
    public bool raceFinished = false;
    private bool startRaceStateSet = false;
    private bool isSlipping = false;
    private float saveSteeringStrength;
    private float currentRotation;

    //Binding if using On-Screencontrolls (Main Method)
    public InputAction joystick = new InputAction("look", binding: "<Gamepad>/leftStick");
    

    private Vector3 Movement;
    
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        //Binding if using Keyboard/ Editor
        joystick.AddCompositeBinding("Dpad")
            .With("Up", "<Keyboard>/w")
            .With("Down", "<Keyboard>/s")
            .With("Left", "<Keyboard>/a")
            .With("Right", "<Keyboard>/d");

        joystick.Enable();
        //gamepad = Gamepad.current;
    }
    
    private void FixedUpdate()
    {
        currentRotation = transform.rotation.y;
        currentRotation += (Mathf.Sin(Time.deltaTime/ 1));
        if (isSlipping)
        {
            transform.Rotate(transform.rotation.x, 440f * Time.deltaTime, transform.rotation.z);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        // is on top check
        isOnTop = Physics.Raycast(transform.position, Vector3.up, RayDistance);
        Debug.Log("Treffer: " + isOnTop);
        
        if (isOnTop)
        {
            timer = 0.0f;
            //return;
        }

        timer += Time.deltaTime;
        if (timer > 3.0f)
        {
            Debug.Log("RESPAWN!");
            respawn();

            timer = timer - 3.0f; // reset timer
        }
        
        if(controllsAllowed){
            if(!startRaceStateSet){
                playerAnimations.SetBool("startRace", true);
            }
            // Movement
            Movement +=  transform.forward * velocity * joystick.ReadValue<Vector2>().y * Time.deltaTime;
            transform.position += Movement * Time.deltaTime;
            
            // Steering
            float steerInput = joystick.ReadValue<Vector2>().x;
            transform.Rotate(Vector3.up * steerInput * Movement.magnitude * steeringStrength * Time.deltaTime);
            
            // Drag
            Movement *= dragStrength;
            Movement = Vector3.ClampMagnitude(Movement, maxVelocity);

            //In one of 10000 Tests the piggy should hit the HonkingHorn randomly
            int randomHonkingHorn = Random.Range(0, 10001);
            if(randomHonkingHorn == 10000){
                playerAnimations.SetTrigger("honkingHorn");
            }
            //Debug.Log("Velocity: " + rigidbody.velocity);
        }

        if(raceFinished){
            playerAnimations.SetBool("raceWon", true);
            //for now, later check for position of player and else set "raceLost"
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
    }

    public void respawn()
    {
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
        saveSteeringStrength = steeringStrength;
        isSlipping = true;
        steeringStrength = 0;
        yield return new WaitForSeconds(1.3f);
        isSlipping = false;
        steeringStrength = saveSteeringStrength;
    }
}
