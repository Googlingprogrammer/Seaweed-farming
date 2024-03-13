using UnityEngine;
using System.Collections;


public class FishMovement : MonoBehaviour
{
    public float speed = 5f; // Speed of movement
    public float switchDirectionTimeMin = 1f; // Minimum time to switch direction
    public float switchDirectionTimeMax = 2f; // Maximum time to switch direction

    private Vector3 moveDirection; // Current movement direction
    private float switchDirectionTimer; // Timer to switch direction
    private float moveTimer; // Timer for moving in the current direction

    void Start()
    {
        // Start the movement coroutine
        StartCoroutine(MoveCoroutine());
    }

    IEnumerator MoveCoroutine()
    {
        while (true)
        {
            // Set a random direction
            moveDirection = Random.insideUnitSphere;

            // Reset move timer
            moveTimer = Random.Range(3f, 5f); // Random duration for moving in one direction

            // Reset switch direction timer
            switchDirectionTimer = Random.Range(switchDirectionTimeMin, switchDirectionTimeMax);

            // Rotate the fish towards the new direction
            transform.rotation = Quaternion.LookRotation(moveDirection, Vector3.up);

            // Wait for a random interval before changing direction
            yield return new WaitForSeconds(Random.Range(1f, 2f));

            while (moveTimer > 0f)
            {
                // Move in the current direction
                transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);

                // Decrease move timer
                moveTimer -= Time.deltaTime;

                yield return null;
            }

            yield return null;
        }
    }

    void Update()
    {
        // Countdown the switch direction timer
        switchDirectionTimer -= Time.deltaTime;

        // If it's time to switch direction, start a new coroutine
        if (switchDirectionTimer <= 0f)
        {
            StartCoroutine(MoveCoroutine());
        }
    }
}
