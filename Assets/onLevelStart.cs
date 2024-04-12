using UnityEngine;
using TMPro;
using System.Collections;

public class OnLevelStart : MonoBehaviour
{
    public GameObject textBoxGameObject; // Reference to the GameObject containing the script with the textToDisplay variable
    public string newText = "Harvest the glowing seaweed"; // The new text to be assigned to textToDisplay

    // Start is called before the first frame update
    void Start()
    {
        // Start the coroutine to modify the text
        StartCoroutine(ModifyText());
    }

    // Coroutine to modify the textToDisplay variable and revert it after 3 seconds
    private IEnumerator ModifyText()
    {
        // Modify the textToDisplay variable
        ModifyTextToDisplay();

        // Wait for 3 seconds
        yield return new WaitForSeconds(3f);

        // Revert newText to an empty string
        newText = "";

        // Modify the textToDisplay variable again to apply the empty string
        ModifyTextToDisplay();
    }

    // Function to modify the textToDisplay variable
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
}
