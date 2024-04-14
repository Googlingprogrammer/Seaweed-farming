using UnityEngine;
using TMPro;
using System.Collections;


public class playerPickup : MonoBehaviour
{
    public Transform attachPoint;
    public GameObject prefabToSpawn;
    public GameObject enableAfter5Seconds;
    public GameObject SonarDisc;
    public AudioSource honkSound;

    private GameObject currentPickup;
    private Rigidbody currentPickupRb;
    private bool isEnlarging = false; // Flag to track if the SonarDisc is currently enlarging
    public float pickupRange = 10f;

    public GameObject textBoxGameObject; // Reference to the GameObject containing the script with the textToDisplay variable
    public string newText = "Honk to shoo away fishes";
    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            // Check if the player is not already carrying an object
            if (currentPickup == null)
            {
                PickUpObject();
            }
            else
            {
                // DropObject();
            }
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            HonkFishes();
        }
    }

    void PickUpObject()
    {
        // Raycast to detect pickupable objects
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, pickupRange))
        {
            // Check if the hit object is on the layer "destroyOnPickup"
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("destroyOnPickup"))
            {
                // Destroy the hit object
                Destroy(hit.collider.gameObject);

                // Instantiate the prefab at the attachPoint
                if (prefabToSpawn != null && attachPoint != null)
                {
                    Instantiate(prefabToSpawn, attachPoint.position, attachPoint.rotation, attachPoint);
                }
                if (Random.value < 0.5f){
                    if (enableAfter5Seconds != null)
                {
                    activateFishes();
                }
                }
                
            }
            else if (hit.collider.gameObject.layer != LayerMask.NameToLayer("Default"))
            {
                // Set the current pickup to the hit object
                currentPickup = hit.collider.gameObject;

                //disable physics
                currentPickupRb = currentPickup.GetComponent<Rigidbody>();
                if (currentPickupRb != null)
                {
                    // currentPickupRb.isKinematic = true;
                    currentPickupRb.constraints = RigidbodyConstraints.None;
                }

                // Attach the pickup to the attachPoint
                currentPickup.transform.parent = attachPoint;
                currentPickup.transform.localPosition = Vector3.zero;
                currentPickup.transform.localRotation = Quaternion.identity;

            }
        }
    }

    public void activateFishes()
{
    newText = "Honk to shoo away fishes";
    ModifyTextToDisplay();
    // Enable the GameObject
    if (enableAfter5Seconds != null)
    {
        enableAfter5Seconds.SetActive(true);
    }

    // Access the PlayerMovement script attached to the same GameObject
    PlayerMovement playerMovement = GetComponent<PlayerMovement>();
    
    // Check if the script was found
    if (playerMovement != null)
    {
        // Set the walkSpeed variable to 0
        playerMovement.walkSpeed = 0f;
    }
}
public void HonkFishes()
{
    if (enableAfter5Seconds != null)
    {
        enableAfter5Seconds.SetActive(false);
    }

    PlayerMovement playerMovement = GetComponent<PlayerMovement>();

    // Check if the script was found
    if (playerMovement != null)
    {
        // Set the walkSpeed variable to 24
        playerMovement.walkSpeed = 24f;
    }

    if (honkSound != null)
    {
        honkSound.Play();
    }

    // Set the textToDisplay variable to the desired value
    newText = "";
    ModifyTextToDisplay();

    if (!isEnlarging)
    {
        StartCoroutine(EnlargeSonarDisc());
    }
}
IEnumerator EnlargeSonarDisc()
{
    isEnlarging = true;
    Vector3 originalScale = SonarDisc.transform.localScale;
    Vector3 targetScale = originalScale * 1.5f; // Scale to 1.5 times the original size
    float duration = 0.2f; // Adjust duration as needed

    float timer = 0f;

    while (timer < duration)
    {
        float t = timer / duration;
        SonarDisc.transform.localScale = Vector3.Lerp(originalScale, targetScale, t);
        timer += Time.deltaTime;
        yield return null;
    }

    // Ensure final scale is exactly the target scale
    SonarDisc.transform.localScale = targetScale;

    // Wait for a short delay (if needed)
    yield return new WaitForSeconds(0.1f);

    // Shrink back to the original size
    timer = 0f;
    while (timer < duration)
    {
        float t = timer / duration;
        SonarDisc.transform.localScale = Vector3.Lerp(targetScale, originalScale, t);
        timer += Time.deltaTime;
        yield return null;
    }

    // Ensure final scale is exactly the original scale
    SonarDisc.transform.localScale = originalScale;

    isEnlarging = false;
}



    private void ModifyTextToDisplay()
    {
        // Check if the reference to the GameObject is assigned
        if (textBoxGameObject != null)
        {
            // Get the script component from the referenced GameObject
            textOsccilate scriptComponent = textBoxGameObject.GetComponent<textOsccilate>();

            // Check if the script component is found
            if (scriptComponent != null)
            {
                // Set the textToDisplay variable to the desired value
                scriptComponent.textToDisplay = newText;
            }
            else
            {
                Debug.LogError("Script component not found on the referenced GameObject!");
            }
        }
        else
        {
            Debug.LogError("GameObject reference is not assigned!");
        }
    }


    public void DropObject()
    {
        // Detach the current pickup from the attachPoint
        currentPickup.transform.parent = null;

        // Re-enable physics for the dropped object
        if (currentPickupRb != null)
        {
            // currentPickupRb.isKinematic = false;
        }

        // Reset the current pickup variable
        currentPickup = null;
    }
}
