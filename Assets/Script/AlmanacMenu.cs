using UnityEngine;
using UnityEngine.UI;

public class AlmanacMenu : MonoBehaviour
{
    [SerializeField] private GameObject almanacMenu;
    public Button BookBtn;

    private void Start()
    {
        BookBtn.onClick.AddListener(TurnOn);
    }

    private void TurnOn()
    {
        if (!almanacMenu.activeSelf) // Only turn it on if it's currently inactive
        {
            almanacMenu.SetActive(true);
            Time.timeScale = 0f; // Pause the game when Almanac is opened
        }
    }
}
