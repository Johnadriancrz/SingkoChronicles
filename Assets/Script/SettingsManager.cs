using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{

    [SerializeField] Slider volumeSlider;

    private void Start()
    {
        // Check if the PlayerPrefs key exists, if not, set a default value
        if (!PlayerPrefs.HasKey("musicVolume")) 
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else 
        { 
            Load();
        }
    }

    /// This method is called when the volume slider value changes
    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    private void Load()
    {
        // Set the slider value to the saved volume level
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void Save()
    {
        // This will persist even after the game is closed
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }

}
