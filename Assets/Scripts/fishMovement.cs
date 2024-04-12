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

    private Renderer fishRenderer; // Renderer component of the fish
    private Material originalMaterial;

     public Material runawayMaterial; // The material to apply while fish are running away

    void Start()
    {
        // Start the movement coroutine
        StartCoroutine(MoveCoroutine());

        // Get the Renderer component of the fish
        fishRenderer = GetComponentInChildren<Renderer>();

        // Store the original material
        originalMaterial = fishRenderer.material;
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

                //fish stay above y 10

                if (transform.position.y < 10f)
                {
                    transform.position = new Vector3(transform.position.x, 10f, transform.position.z);
                }

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

    public void FishRunAway(Vector3 playerPosition)
    {
        Debug.Log("AHH!");

        fishRenderer.material = runawayMaterial;//change fish material

        StartCoroutine(RunawayCoroutine(playerPosition));
    }
     IEnumerator RunawayCoroutine(Vector3 playerPosition)
    {
        // Calculate direction away from the player
        Vector3 runDirection = transform.position - playerPosition;
        runDirection.y = 0f; // Ensure the fish stays at the same height

        // Normalize the direction to get a consistent speed
        runDirection.Normalize();

        float elapsedTime = 0f;

        // Move away from the player for 2 seconds
        while (elapsedTime < 5f)
        {
            // Move in the current direction
            transform.Translate(runDirection * speed * Time.deltaTime, Space.World);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Revert back to the original material
        fishRenderer.material = originalMaterial;
    }
}
