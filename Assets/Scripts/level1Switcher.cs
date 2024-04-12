using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelSwitcher : MonoBehaviour
{
    public string newLevelName = "Level 2"; // Name of the new level to load
    public GameObject seaweedCollectorObject; // Reference to the GameObject with SeaweedCollector script

    void Start()
    {
        StartCoroutine(CheckSeaweedCollected());
    }

    IEnumerator CheckSeaweedCollected()
    {
        // Wait for 1 second before starting to check seaweedCollected
        yield return new WaitForSeconds(1f);

        // Continuously check seaweedCollected
        while (true)
        {
            SeaweedCollector collectorScript = seaweedCollectorObject.GetComponent<SeaweedCollector>();

            if (collectorScript != null && collectorScript.seaweedCollected >= 5)
            {
                // If seaweedCollected reaches 5, switch to the new level
                StartCoroutine(SwitchLevelAfterDelay());
                yield break; // Exit the coroutine
            }

            yield return null; // Wait for the next frame
        }
    }

    IEnumerator SwitchLevelAfterDelay()
    {
        // Wait for 5 seconds
        yield return new WaitForSeconds(5f);

        // Load the new level
        SceneManager.LoadScene(newLevelName);
    }
}
