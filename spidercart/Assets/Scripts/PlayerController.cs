using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float velocity = 50;
    [SerializeField] private float maxVelocity = 15;
    [SerializeField] private float dragStrength = 0.99f;
    [SerializeField] private float steeringStrength = 20;
    [SerializeField] private Animator playerAnimations = null;


    //Use to disbale controlls before start and after race has finished, is used in GameStateController
    public bool controllsAllowed = false;
    public bool raceFinished = false;

    private bool startRaceStateSet = false;
    

    private Vector3 Movement;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(controllsAllowed){
            if(!startRaceStateSet){
                playerAnimations.SetBool("startRace", true);
            }
            // Movement
            Movement +=  transform.forward * velocity * Input.GetAxis("Vertical") * Time.deltaTime;
            transform.position += Movement * Time.deltaTime;
            
            // Steering
            float steerInput = Input.GetAxis("Horizontal");
            transform.Rotate(Vector3.up * steerInput * Movement.magnitude * steeringStrength * Time.deltaTime);
            
            // Drag
            Movement *= dragStrength;
            Movement = Vector3.ClampMagnitude(Movement, maxVelocity);

            //In one of 10000 Tests the piggy should hit the HonkingHorn randomly
            int randomHonkingHorn = Random.Range(0, 10001);
            if(randomHonkingHorn == 10000){
                playerAnimations.SetTrigger("honkingHorn");
            }
        }

        if(raceFinished){
            playerAnimations.SetBool("raceWon", true);
            //for now, later check for position of player and else set "raceLost"
        }
    }
}
