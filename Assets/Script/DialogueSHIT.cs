using UnityEngine;

public class DialogueSHIT : MonoBehaviour
{

    public GameObject dia;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player") && PlayerPositionData.currentNpc == 0)
        {
            dia.SetActive(true);
            PlayerPositionData.currentNpc++;
            //Time.timeScale = 0f; // Pause the game
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            dia.SetActive(false);
            //Time.timeScale = 1f; // Resume the game
        }
    }

}
