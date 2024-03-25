using UnityEngine;
using TMPro;

public class trackerTextBox : MonoBehaviour
{
    public SeaweedCollector seaweedCollector; // Reference to the SeaweedCollector script
    public TextMeshProUGUI textField; // Public reference to the TextMeshProUGUI component

    void Update()
    {
        if (seaweedCollector != null && textField != null)
        {
            textField.text = "Seaweed Collected: " + seaweedCollector.seaweedCollected + "/5"; // Update the text with the value of seaweedCollected
        }
    }
}
