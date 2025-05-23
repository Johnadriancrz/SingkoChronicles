using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class AudioManager : MonoBehaviour
{
    [Header("------- Audio Source--------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;


    [Header("------- Audio Clip--------")]
    public AudioClip background;
    public AudioClip walking;
    public AudioClip Clicking;

    private static AudioManager instance;

    private float lastSFXTime = 0f; 
    private float sfxCooldown = 0.1f;

    
    private void Awake()
    {
        // Singleton pattern to ensure only one instance of AudioManager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep this object alive across scenes
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    private void Start()
    {
        // Enable looping for the background music
        if (musicSource != null && background != null)
        {
            musicSource.clip = background;
            musicSource.loop = true; 
            musicSource.Play();
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // If it hit your target scene name the bg music will stop playing
        if (scene.name == "Front") 
        {
            musicSource.Stop();
        }
        else if (!musicSource.isPlaying)
        {
            musicSource.Play(); 
        }
    }


    /// Function to play background music
    public void PlaySFX(AudioClip clip)
    {
        // Check if the SFXSource and clip are not null
        if (SFXSource != null && clip != null)
        {
            // Check if the cooldown period has passed
            if (Time.time - lastSFXTime >= sfxCooldown)
            {
                // Play the sound effect
                SFXSource.PlayOneShot(clip);
                lastSFXTime = Time.time; 
            }
        }
    }
}


