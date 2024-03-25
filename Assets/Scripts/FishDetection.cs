using UnityEngine;
using System.Collections;

public class FishDetection : MonoBehaviour
{
    public LayerMask fishLayer; // Layer mask to filter which objects can be detected as fish
    public float detectionRadius = 10f; // Radius within which to detect fish
    public float abilityCooldown = 2f; // Cooldown duration for the ability

    private bool abilityReady = true; // Flag to track if the ability is ready to be used

    void Update()
    {
        // Check if the player pressed the "E" key and the ability is ready
        if (Input.GetKeyDown(KeyCode.E) && abilityReady)
        {
            // Cast a sphere to detect nearby objects
            Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, fishLayer);

            // Check each collider detected
            foreach (Collider collider in colliders)
            {
                // Check if the detected object has the "LeFishe" tag
                if (collider.CompareTag("LeFishe"))
                {
                    // Trigger the FishRunAway function in FishMovement script if it exists on the detected object
                    FishMovement fishMovementScript = collider.GetComponent<FishMovement>();
                    if (fishMovementScript != null)
                    {
                        fishMovementScript.FishRunAway(transform.position);
                    }
                }
            }

            // Start the ability cooldown
            StartCoroutine(AbilityCooldown());
        }
    }

    IEnumerator AbilityCooldown()
    {
        // Set the ability flag to false
        abilityReady = false;

        // Wait for the cooldown duration
        yield return new WaitForSeconds(abilityCooldown);

        // Set the ability flag to true
        abilityReady = true;
    }

    // Visualize the detection radius in the Unity Editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
