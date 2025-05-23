using UnityEngine;
using UnityEngine.UI;

public class MonsterBackBtn : MonoBehaviour
{
    public GameObject CreaturesCanvas; // Reference to the canvas
    public Button BackButton; // Reference to the back button

    private void Start()
    {
        BackButton.onClick.AddListener(CloseCreaturesCanvas);
    }

    public void CloseCreaturesCanvas()
    {
        CreaturesCanvas.SetActive(false);
        Time.timeScale = 1f;
    }
}
