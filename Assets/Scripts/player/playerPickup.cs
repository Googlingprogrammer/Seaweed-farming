using UnityEngine;

public class playerPickup : MonoBehaviour
{
    public Transform attachPoint;
    public GameObject prefabToSpawn;

    private GameObject currentPickup;
    private Rigidbody currentPickupRb;
    public float pickupRange = 10f;

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
