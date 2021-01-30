using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerController : NetworkBehaviour
{
    public float Speed;
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
        if (Direction.Equals(Vector2.zero))
        {
            RigidBody.velocity = Vector3.zero;
        }

        if (!isLocalPlayer)
        {
            return;
        }

        var direction = (Vector3.right * Direction.x + Vector3.forward * Direction.y).normalized;
        RigidBody.velocity = direction * Speed * Time.deltaTime;

        if (direction.magnitude >= 0.1) {
            RigidBody.rotation = Quaternion.LookRotation(direction);
        }
    }
}
