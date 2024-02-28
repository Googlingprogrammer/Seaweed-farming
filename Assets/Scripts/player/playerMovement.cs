using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float walkSpeed = 12f;
    public float runSpeed = 24f; // Double the walk speed for running
    public float movementSpeed; // Current movement speed
    public float jumpHeight = 3f;
    public float gravity = 0;
     Vector3 velocity;
    
    void Update()
    {

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        float y = Input.GetButtonDown("Jump") ? 1.0f : 0.0f;

        Vector3 move = transform.right * x + transform.forward * z + transform.up * y;

        // Check if the player is holding the "Shift" key to run
        movementSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        controller.Move(move * movementSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
