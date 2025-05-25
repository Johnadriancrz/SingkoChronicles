using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;
using System.Data.SqlTypes;

public class NpcDialogueManager : MonoBehaviour
{
    public GameObject Spawn;
    public GameObject uiPanel;
    public GameObject interactionUI;
    public GameObject sceneTransitionUI;
    public GameObject dialogueBox;
    public TMP_Text dialogueText;
    public TMP_Text continueText;
    public float typingSpeed = 0.05f;
    public GameObject sceneTransitionButton;
    public string sceneToLoad;
    public GameObject creature;

    private string valueToTransfer = "";
    public string spawnPointID = "";
    public static string stringToTransfer;

    [Header("Dialogue Setup")]
    public List<string> dialogues; // First visit dialogue

    [Header("NPC Unique Key")]
    public string npcId = "npc1";

    [Header("Testing - Check if reset or Uncheck if Normal Gameplay")]
    public bool resetOnStart = false;

    private int dialogueIndex = 0;
    private bool isPlayerNear = false;
    private bool isTyping = false;
    private bool dialogueActive = false;
    private bool dialoguePlayed = false;

    [Header("Return Dialogue")]
    public List<string> returnDialogues;
    private bool hasVisitedBefore = false;
    private int returnDialogueIndex = 0;
    private bool returnDialogueActive = false;

    private int counter = 0;
    void Start()
    {

        uiPanel.SetActive(true);
        interactionUI.SetActive(false);
        sceneTransitionUI.SetActive(false);
        dialogueBox.SetActive(false);
        continueText.gameObject.SetActive(false);
        sceneTransitionButton.SetActive(false);

        sceneTransitionButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(LoadNextScene);

        // 1. Check if the scene was visited before
        hasVisitedBefore = PlayerPrefs.GetInt("HasVisited_" + SceneManager.GetActiveScene().name, 0) == 1;

        // 2. Check if the NPC dialogue was played
        dialoguePlayed = PlayerPrefs.GetInt("NpcDialoguePlayed_" + npcId, 0) == 1;

        // 3. If returning, play returnDialogues automatically
        if (hasVisitedBefore && returnDialogues != null && returnDialogues.Count > 0)
        {
            dialogueBox.SetActive(true);
            returnDialogueActive = true;
            returnDialogueIndex = 0;
            StartCoroutine(TypeReturnDialogue());
            interactionUI.SetActive(false);
            isPlayerNear = false;
        }
    }



    void Update()
    {

        if (counter == 0)
        {
            PlayerPrefs.DeleteKey("NpcDialoguePlayed_" + npcId);
            PlayerPrefs.DeleteKey("HasVisited_" + SceneManager.GetActiveScene().name);
            PlayerPrefs.Save();
            counter++;
        }

        // Handle return dialogue
        if (returnDialogueActive && Input.GetMouseButtonDown(0) && !isTyping)
        {
            continueText.gameObject.SetActive(false);
            returnDialogueIndex++;
            if (returnDialogueIndex < returnDialogues.Count)
            {
                StartCoroutine(TypeReturnDialogue());
            }
            else
            {
                dialogueBox.SetActive(false);
                returnDialogueActive = false;
                sceneTransitionUI.SetActive(true);
                sceneTransitionButton.SetActive(false);
            }
            return; // Prevent further checks this frame
        }

        // Only allow interaction if not returning and not already played
        if (isPlayerNear && !dialogueActive && !dialoguePlayed && !hasVisitedBefore)
        {
            interactionUI.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartDialogue();
            }
        }
        else
        {
            interactionUI.SetActive(false);
        }

        //speed ng dialogue
        if (dialogueActive && Input.GetMouseButtonDown(0) && !isTyping)
        {
            ShowNextDialogue();
        }
    }

    IEnumerator TypeReturnDialogue()
    {
        isTyping = true;
        dialogueText.text = "";
        continueText.gameObject.SetActive(false);

        foreach (char letter in returnDialogues[returnDialogueIndex])
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
        continueText.gameObject.SetActive(true);
    }
    // Set the desired spawn point before returning


    void StartDialogue()
    {
        if (dialogues.Count == 0) return;

        dialogueBox.SetActive(true);
        interactionUI.SetActive(false);
        dialogueActive = true;
        dialoguePlayed = true;
        PlayerPrefs.SetInt("NpcDialoguePlayed_" + npcId, 1); // Save with unique key
        PlayerPrefs.Save();
        dialogueIndex = 0;
        StartCoroutine(TypeDialogue());
    }

    IEnumerator TypeDialogue()
    {
        isTyping = true;
        dialogueText.text = "";
        continueText.gameObject.SetActive(false);

        foreach (char letter in dialogues[dialogueIndex])
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
        continueText.gameObject.SetActive(true);


    }

    void ShowNextDialogue()
    {
        continueText.gameObject.SetActive(false);
        dialogueIndex++;

        if (dialogueIndex < dialogues.Count)
        {
            StartCoroutine(TypeDialogue());
        }
        else
        {
            dialogueBox.SetActive(false);
            dialogueActive = false;
            sceneTransitionUI.SetActive(true);
            sceneTransitionButton.SetActive(true);

            // Mark the scene as visited after the first dialogue is finished
            if (!hasVisitedBefore)
            {
                PlayerPrefs.SetInt("HasVisited_" + SceneManager.GetActiveScene().name, 1);
                PlayerPrefs.Save();
            }
        }
    }

    void LoadNextScene()
    {

        Debug.Log("Interacted! - " + spawnPointID);
        PlayerPositionData.returnSceneName = SceneManager.GetActiveScene().name;// <-- ito ang magse-set ng tamang scene name
        PlayerPositionData.nextSpawnPointID = spawnPointID;

        valueToTransfer = creature.name;
        NpcDialogueManager.stringToTransfer = valueToTransfer;

        PlayerPositionData.currentNpc++;

        SceneManager.LoadScene(sceneToLoad);
    }

    

    void OnTriggerEnter2D(Collider2D other)
    {

            var sp = Spawn.GetComponent<SpawnPoint>();
            if (sp != null)
        {
            spawnPointID = sp.spawnID; // <-- ito ang magse-set ng tamang spawn ID
        }

        String name = SceneManager.GetActiveScene().name;
        int currScene = 0;

        if(name.Equals("LevelOne"))
        {
            currScene = 1;
        }
        else if (name.Equals("LevelTwo"))
        {
            currScene = 2;
        }
        else if (name.Equals("Levelthree"))
        {
            currScene = 3;
        }

        if ((other.CompareTag("Player") && !dialoguePlayed) && currScene == PlayerPositionData.currentNpc)
        {
            isPlayerNear = true;
        }

        Debug.Log("current scne! " + currScene);
        Debug.Log("counter scne! " + PlayerPositionData.currentNpc);

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }
}
