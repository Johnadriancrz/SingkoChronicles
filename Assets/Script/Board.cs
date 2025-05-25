using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class Board : MonoBehaviour
{
    public static string stringToTransfer;

    private static readonly KeyCode[] SUPPORTED_KEYS = new KeyCode[]
    {
        KeyCode.A, KeyCode.B, KeyCode.C, KeyCode.D, KeyCode.E, KeyCode.F, KeyCode.G, KeyCode.H,
        KeyCode.I, KeyCode.J, KeyCode.K, KeyCode.L, KeyCode.M, KeyCode.N, KeyCode.O, KeyCode.P,
        KeyCode.Q, KeyCode.R, KeyCode.S, KeyCode.T, KeyCode.U, KeyCode.V, KeyCode.W, KeyCode.X,
        KeyCode.Y, KeyCode.Z,
    };

    private Row[] rows;
    private string[] solutions;
    private string[] validWords;

    [Header("Alerts")]
    public GameObject alertLettersPanel;
    public GameObject tryAgainPanel;
    public GameObject CompletePanel;
    public Button alertLettersOkButton;

    [Header("Word Input")]
    public string word; 

    private int rowIndex = 0;
    private int columnIndex = 0;

    [Header("Tile States")]
    public Tile.TileState emptyState;
    public Tile.TileState occupiedState;
    public Tile.TileState correctState;
    public Tile.TileState wrongSpotState;
    public Tile.TileState incorrectState;

    [Header("UI")]
    public TextMeshProUGUI invalidWordText;
    public Button newWordButton;
    public Button tryAgainButton;

    private void Awake()
    {
        // Get all the rows in the board
        rows = GetComponentsInChildren<Row>();

        if (rows == null || rows.Length == 0)
        {
            Debug.LogError("Rows are not initialized properly!");
        }
    }

    private void Start()
    {
        LoadData();
        SetRandomWord(); // Ensuring word is set at game start

        word = NpcDialogueManager.stringToTransfer;

        //Alert Button Close
        if (alertLettersOkButton != null)
            alertLettersOkButton.onClick.AddListener(HideAlertLettersPanel);
    }

    public void NewGame()
    {
        ClearBoard();
        SetRandomWord(); // Set a new word when the game restarts
        enabled = true;
    }

    public void TryAgain()
    {
        ClearBoard();
        SetRandomWord(); // Ensure a new word is set when retrying
        enabled = true;
    }

    private void LoadData()
    {
        // Load the word lists from Resources folder with null checks
        TextAsset textFile = Resources.Load("official_wordle_all") as TextAsset;
        if (textFile == null)
        {
            Debug.LogError("File 'official_wordle_all' not found in Resources!");
            return;
        }
        validWords = textFile.text.Split('\n');

        textFile = Resources.Load("official_wordle_common") as TextAsset;
        if (textFile == null)
        {
            Debug.LogError("File 'official_wordle_common' not found in Resources!");
            return;
        }
        solutions = textFile.text.Split('\n');
    }

    public void SetRandomWord()
    {
        Debug.Log("Word set from Inspector: " + word);
    }

    private void Update()
    {
        if (rows == null || rowIndex >= rows.Length) return; // Prevent null reference errors

        Row currentRow = rows[rowIndex];

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            if (columnIndex > 0)
            {
                columnIndex = Mathf.Max(columnIndex - 1, 0);
                currentRow.tiles[columnIndex].SetLetter('\0');
                currentRow.tiles[columnIndex].SetState(emptyState);
                invalidWordText.gameObject.SetActive(false);
            }
        }
        else if (columnIndex >= currentRow.tiles.Length && Input.GetKeyDown(KeyCode.Return))
        {
            SubmitRow(currentRow);
        }
        else
        {
            for (int i = 0; i < SUPPORTED_KEYS.Length; i++)
            {
                if (Input.GetKeyDown(SUPPORTED_KEYS[i]))
                {
                    if (columnIndex < currentRow.tiles.Length)
                    {
                        currentRow.tiles[columnIndex].SetLetter((char)SUPPORTED_KEYS[i]);
                        currentRow.tiles[columnIndex].SetState(occupiedState);
                        columnIndex++;
                    }
                    else
                    {
                        if (alertLettersPanel != null)
                            alertLettersPanel.SetActive(true);
                    }
                    break;
                }
            }
        }
    }

    public void HideAlertLettersPanel()
    {
        if (alertLettersPanel != null)
            alertLettersPanel.SetActive(false);
    }

    public void SubmitRow(Row row)
    {
        if (row == null || row.tiles == null || row.tiles.Length == 0)
        {
            Debug.LogError("Row or tiles are not properly initialized!");
            return;
        }

        if (string.IsNullOrEmpty(word) || word.Length != row.tiles.Length)
        {
            Debug.LogError("Word is not set or length mismatch!");
            return;
        }

        string remainingWord = word;

        for (int i = 0; i < row.tiles.Length; i++)
        {
            Tile tile = row.tiles[i];

            if (tile.letter == word[i])
            {
                tile.SetState(correctState);
                remainingWord = remainingWord.Remove(i, 1).Insert(i, " ");
            }
            else if (!word.Contains(tile.letter.ToString()))
            {
                tile.SetState(incorrectState);
            }
        }

        for (int i = 0; i < row.tiles.Length; i++)
        {
            Tile tile = row.tiles[i];

            if (tile.State != correctState && tile.State != incorrectState)
            {
                int index = remainingWord.IndexOf(tile.letter.ToString());

                if (index >= 0)
                {
                    tile.SetState(wrongSpotState);
                    remainingWord = remainingWord.Remove(index, 1).Insert(index, " ");
                }
                else
                {
                    tile.SetState(incorrectState);
                }
            }
        }

        if (HasWon(row))
        {
            Debug.Log("You Win!");
            CompletePanel.SetActive(true);
            enabled = false;
        }

        rowIndex++;
        columnIndex = 0;

        if (rowIndex >= rows.Length)
        {
            Debug.Log("Game Over");
            enabled = false;
        }
    }

    private bool HasWon(Row row)
    {
        for (int i = 0; i < row.tiles.Length; i++)
        {
            if (row.tiles[i].State != correctState)
            {
                return false;
            }
        }
        return true;
    }

    private void ClearBoard()
    {
        if (rows == null) return;

        for (int i = 0; i < rows.Length; i++)
        {
            for (int col = 0; col < rows[i].tiles.Length; col++)
            {
                rows[i].tiles[col].SetLetter('\0');
                rows[i].tiles[col].SetState(emptyState);
            }
        }
        rowIndex = 0;
        columnIndex = 0;
    }

    private void OnEnable()
    {
        tryAgainButton.gameObject.SetActive(false);
        tryAgainPanel.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        tryAgainButton.gameObject.SetActive(true);
        tryAgainPanel.gameObject.SetActive(true);
    }
}
