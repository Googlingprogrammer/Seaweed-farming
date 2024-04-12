using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float walkSpeed = 12f;
    public float runSpeed = 24f; // Double the walk speed for running
    // public float jumpHeight = 3f;
    // public float gravity = 9.8f;

    Vector3 velocity;

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        float y = 0f;

        // if (Input.GetKey(KeyCode.Space))
        // {
        //     y = 1.0f; // Going up
        // }
        // else if (Input.GetKey(KeyCode.LeftControl))
        // {
        //     y = -1.0f; // Going down
        // }

        Vector3 move = transform.right * x + transform.forward * z + transform.up * y;

        // Check if the player is holding the "Shift" key to run
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        // Move the controller
        controller.Move((move * currentSpeed + velocity) * Time.deltaTime);
    }
}
