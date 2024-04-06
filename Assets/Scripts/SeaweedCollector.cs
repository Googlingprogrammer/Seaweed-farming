using UnityEngine;

public class SeaweedCollector : MonoBehaviour
{
    public int seaweedCollected = 0; // Public integer to keep track of collected seaweeds

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("AAA");
        if (other.CompareTag("WinCon")) // Check if the collider has collided with an object tagged "seaweed"
        {
            Destroy(other.gameObject); // Destroy the seaweed object
            seaweedCollected++; // Increment the seaweed collected count
        }
    }
}
