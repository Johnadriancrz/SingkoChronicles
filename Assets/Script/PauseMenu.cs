using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f; // Pause the game
    }

    public void Home()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f; // Resume the game
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f; // Resume the game
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f; // Resume the game
    }
}
