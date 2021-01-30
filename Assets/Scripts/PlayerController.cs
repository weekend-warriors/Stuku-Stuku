using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerController : NetworkBehaviour
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
        if (!isLocalPlayer)
        {
            return;
        }

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        rigidBody.velocity = (Vector3.right * horizontalInput + Vector3.forward * verticalInput) * moveVelocity * Time.fixedDeltaTime;
    }
}
