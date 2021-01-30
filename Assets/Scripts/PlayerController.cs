using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveVelocity;
    private Rigidbody rigidBody;
    private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDirection;

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
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        moveDirection = (Vector3.right * horizontalInput + Vector3.forward * verticalInput).normalized;
        rigidBody.velocity = moveDirection * moveVelocity * Time.fixedDeltaTime;
        if (moveDirection.magnitude >= 0.1)
            transform.rotation = Quaternion.LookRotation(moveDirection);
    }
}
