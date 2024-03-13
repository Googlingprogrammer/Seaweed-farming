using UnityEngine;
using TMPro;

public class victoryScreen : MonoBehaviour
{
    public int winCon = 10;

    public string victoryText = "WOOHOO";
    public SeaweedCollector seaweedCollector; // Reference to the SeaweedCollector script
    public TextMeshProUGUI textField; // Public reference to the TextMeshProUGUI component

    void Update()
    {
        if (seaweedCollector != null && textField != null)
        {
            if (seaweedCollector.seaweedCollected >= winCon) {
                textField.text = victoryText; // update the text
            }
        }
    }
}
