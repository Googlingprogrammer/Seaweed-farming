using UnityEngine;

public class waterCurrent : MonoBehaviour
{
    public Transform targetObject;  // The GameObject the player will be attracted to
    public float attractionForce = 1f;  // Strength of the attraction force
    public float lerpSpeed = 0.1f;  // Speed of lerping towards the target

    private CharacterController characterController;

    void Start()
    {
        // Get the CharacterController component of the player
        characterController = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        if (targetObject != null)
        {
            // Calculate the direction from player to target
            Vector3 directionToTarget = targetObject.position - transform.position;

            // Calculate the distance to the target
            float distanceToTarget = directionToTarget.magnitude;

            // Normalize the direction to get a unit vector
            Vector3 normalizedDirection = directionToTarget.normalized;

            // Calculate the attraction force based on distance
            float attractionFactor = attractionForce;

            // Move the player towards the target using lerping
            Vector3 newPosition = Vector3.Lerp(transform.position, targetObject.position, lerpSpeed * Time.deltaTime);

            // Move the player towards the target
            characterController.Move((newPosition - transform.position) * attractionFactor * Time.deltaTime);
        }
    }
}
