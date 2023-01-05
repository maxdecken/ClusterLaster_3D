using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_third_person : MonoBehaviour
{
    [SerializeField] private Transform orientation;
    [SerializeField] private Transform player;
    [SerializeField] private Transform playerObject;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private float rotateSpeed;
    private Vector3 camPosition;
    
    [SerializeField] private Transform lookAt;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        camPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Vector3 viewDirection = lookAt.position - camPosition;
        orientation.forward = viewDirection.normalized;
        transform.position = player.position - player.transform.forward * 10;

        //playerObject.forward = viewDirection.normalized;
    }
}
