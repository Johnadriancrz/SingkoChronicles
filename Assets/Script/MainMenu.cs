using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    private AudioManager audioManager;
    public AudioClip clickSound;

    private void Start()
    {
        // Find the AudioManager instance in the scene
        audioManager = Object.FindAnyObjectByType<AudioManager>();
    }

    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName); // Load the specified scene
        PlayClickSound();   // Play the click sound
    }

    public void QuitApp()
    {
        Application.Quit();
        Debug.Log("Application has quit");
        PlayClickSound();
    }

    public void GoToHome()
    {
        SceneManager.LoadScene(0); // Load the first scene (Home)
        PlayClickSound();
    }

    private void PlayClickSound() 
    {
        // Play the click sound if the AudioManager and the sound clip are not null
        if (audioManager != null && audioManager.Clicking != null)
        {   
            // to avoid overlapping sounds
            audioManager.PlaySFX(audioManager.Clicking);
        }  
    }
}
