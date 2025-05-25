using UnityEngine;
using UnityEngine.SceneManagement;

public class WordleReturnButton : MonoBehaviour
{
    public void ReturnToPreviousScene()
    {
        SceneManager.LoadScene(PlayerPositionData.returnSceneName); // para ma load ang scene kung san siya last nag interact
    }
}