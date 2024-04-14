using UnityEngine;
using System.Collections;
using TMPro;

public class textOsccilate : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public float minFontSize = 36f;
    public float maxFontSize = 72f;
    public float oscillationSpeed = 1f;
    public TextMeshProUGUI textField;
    public string textToDisplay = "";

    // Start is called before the first frame update
    void Start()
    {
        // Start the font size oscillation coroutine
        StartCoroutine(OscillateFontSize());
    }

    void Update()
    {
        if (textField != null)
        {
            textField.text = textToDisplay;
        }
    }

    private IEnumerator OscillateFontSize()
    {
        while (true)
        {
            // Calculate the font size using a sine wave between minFontSize and maxFontSize
            float fontSize = Mathf.Lerp(minFontSize, maxFontSize, Mathf.Sin(Time.time * oscillationSpeed));

            // Assign the calculated font size to the TextMeshPro text component
            textComponent.fontSize = fontSize;

            yield return null;
        }
    }
}
