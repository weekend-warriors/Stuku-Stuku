using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerController : NetworkBehaviour
{
    public float Speed;
    public float RotationSpeed;
    public float GravitySpeed;
    public Animator Animator;
    private Rigidbody RigidBody;
    private Vector2 Direction;

    void Start()
    {
        if (isLocalPlayer)
        {
            Camera.main.GetComponent<SmoothCameraFollow>().Target = transform;
        }

        RigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        // TODO: there is a bug here
        var isStopped = Direction.Equals(Vector2.zero);
        if (isStopped)
        {
            RigidBody.velocity = Vector3.zero;
        }

        Animator.SetBool("IsWalking", !isStopped);

        if (!isLocalPlayer)
        {
            return;
        }

        var direction = (Vector3.right * Direction.x + Vector3.forward * Direction.y).normalized;
        var gravity = new Vector3(0, RigidBody.velocity.y);
        RigidBody.velocity = gravity + (direction * Speed * Time.fixedDeltaTime);

        if (direction.magnitude >= 0.1) {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.fixedDeltaTime * RotationSpeed);
        }
    }
}
