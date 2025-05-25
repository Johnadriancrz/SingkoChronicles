using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public static Timer Instance { get; private set; }

    [SerializeField] TextMeshProUGUI timerText;
    float elapsedTime;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        if (timerText != null)
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void SetTimerText(TextMeshProUGUI newText)
    {
        timerText = newText;
    }

    // --- Add these methods ---
    public float GetElapsedTime()
    {
        return elapsedTime;
    }

    public string GetElapsedTimeString()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}



//1. Para sa float value (seconds): float elapsed -> Timer.Instance.GetElapsedTime();

//2. Para sa formatted string (e.g. "02:15"): -> string elapsedString = Timer.Instance.GetElapsedTimeString();