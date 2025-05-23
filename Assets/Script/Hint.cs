using UnityEngine;
using TMPro;

public class Hint : MonoBehaviour
{
    [Header("Text Input")]
    public string inputText; // Editable in Inspector

    [Header("UI Display")]
    public TextMeshProUGUI displayText; // Assign your TMP UI Text in Inspector

    private void Start()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        if (displayText != null)
        {
            displayText.text = inputText;
        }
        else
        {
            Debug.LogError("TextMeshProUGUI reference is missing!");
        }
    }
}
