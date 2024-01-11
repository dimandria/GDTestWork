using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    public float speed = 2f;
    private Rigidbody _rb;
    public float rotationSpeed = 10f;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        
    }

    private void Update()
    {
       float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 directionVector = new Vector3(horizontal, 0, vertical);
        _rb.AddForce( directionVector * speed);
        transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.LookRotation(directionVector),Time.deltaTime * rotationSpeed ); 
       
    }

   
     
}
