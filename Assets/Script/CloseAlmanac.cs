using UnityEngine;
using UnityEngine.UI;

public class CloseAlmanac : MonoBehaviour
{
    [SerializeField] private GameObject almanacMenu;
    public Button closeButton; // X Button

    private void Start()
    {
        closeButton.onClick.AddListener(TurnOff);
    }

    private void TurnOff()
    {
        if (almanacMenu.activeSelf) // Ensures it only turns off when active
        {
            almanacMenu.SetActive(false);
            Time.timeScale = 1f; // Resume game when Almanac is closed
        }
    }
}
