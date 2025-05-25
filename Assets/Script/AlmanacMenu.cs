using UnityEngine;
using UnityEngine.UI;

public class AlmanacMenu : MonoBehaviour
{
    [SerializeField] private GameObject almanacMenu;
    public Button BookBtn;

    public GameObject creatures1;
    public GameObject creatures2;
    public GameObject creatures3;
    public GameObject Text;

    private void Start()
    {
        BookBtn.onClick.AddListener(TurnOn);

        checkingAlmanac();
    }

    private void TurnOn()
    {
        if (!almanacMenu.activeSelf) // Only turn it on if it's currently inactive
        {
            Debug.Log("ina mo");
            almanacMenu.SetActive(true);
            Time.timeScale = 0f; // Pause the game when Almanac is opened
        }
    }

    void checkingAlmanac()
    {
        if (PlayerPositionData.currentNpc == 2)
        {
            creatures1.SetActive(true);
            creatures2.SetActive(false);
            creatures3.SetActive(false);
        }
        else if (PlayerPositionData.currentNpc == 3)
        {
            creatures1.SetActive(true);
            creatures2.SetActive(true);
            creatures3.SetActive(false);
        }
        else if (PlayerPositionData.currentNpc == 4)
        {
            creatures1.SetActive(true);
            creatures2.SetActive(true);
            creatures3.SetActive(true);
        }
        else
        {
            Text.SetActive(true);
            creatures1.SetActive(false);
            creatures2.SetActive(false);
            creatures3.SetActive(false);
        }
    }
}
