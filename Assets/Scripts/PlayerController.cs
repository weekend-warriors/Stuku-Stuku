using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveVelocity;
    private Rigidbody rigidBody;
    private float horizontalInput;
    private float verticalInput;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() 
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        rigidBody.velocity = (Vector3.right * horizontalInput + Vector3.forward * verticalInput) * moveVelocity * Time.fixedDeltaTime;
    }
}
