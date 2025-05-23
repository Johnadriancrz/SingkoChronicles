using UnityEngine;
using UnityEngine.UI;

public class MonsterSelect : MonoBehaviour
{
    public Button SelectButton; // Button to open the Creatures Canvas
    public GameObject CreaturesCanvas; // Reference to your canvas

    private void Start()
    {
        SelectButton.onClick.AddListener(OpenCreaturesCanvas);
    }

    public void OpenCreaturesCanvas()
    {
        CreaturesCanvas.SetActive(true);
        Time.timeScale = 0f; // Pause the game if necessary when the canvas opens
    }
}
