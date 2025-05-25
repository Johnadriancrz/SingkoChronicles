using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueBox;
    public TMP_Text dialogueText;
    public TMP_Text clickPrompt;
    public float typingSpeed = 0.05f; // Adjust for faster/slower typing effect

    [Header("Dialogues")]
    [TextArea(2, 5)]
    public string[] dialogues;


    private int dialogueIndex = 0;
    private bool isTyping = false;

    void Start()
    {
        PauseGame(); // Pause the game when dialogue starts
        dialogueBox.SetActive(true);
        clickPrompt.gameObject.SetActive(false); // Hide prompt initially
        StartCoroutine(TypeDialogue());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isTyping) // Click to continue
        {
            ShowNextDialogue();
        }
    }

    IEnumerator TypeDialogue()
    {
        isTyping = true;
        dialogueText.text = "";
        clickPrompt.gameObject.SetActive(false); // Hide prompt while typing

        foreach (char letter in dialogues[dialogueIndex])
        {
            dialogueText.text += letter;
            yield return new WaitForSecondsRealtime(typingSpeed); // Use REALTIME to work while paused
        }

        isTyping = false;
        clickPrompt.gameObject.SetActive(true); // Show prompt when finished typing
    }

    void ShowNextDialogue()
    {
        dialogueIndex++;

        if (dialogueIndex < dialogues.Length)
        {
            StartCoroutine(TypeDialogue());
        }
        else
        {
            ResumeGame(); // Resume game when dialogue ends
            dialogueBox.SetActive(false);
            clickPrompt.gameObject.SetActive(false); // Hide prompt when dialogue ends

        }
    }

    void PauseGame()
    {
        Time.timeScale = 0f; // Freeze all game movement and physics, but not UI
    }

    void ResumeGame()
    {
        Time.timeScale = 1f; // Resume the game when dialogue ends
    }
}
