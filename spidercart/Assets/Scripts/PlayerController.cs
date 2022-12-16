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
    

    private Vector3 Movement;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        // Movement
        Movement +=  transform.forward * velocity * Input.GetAxis("Vertical") * Time.deltaTime;
        transform.position += Movement * Time.deltaTime;
        
        // Steering
        float steerInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * steerInput * Movement.magnitude * steeringStrength * Time.deltaTime);
        
        // Drag
        Movement *= dragStrength;
        Movement = Vector3.ClampMagnitude(Movement, maxVelocity);
    }
}
