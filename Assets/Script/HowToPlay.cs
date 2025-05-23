using UnityEngine;
using UnityEngine.SceneManagement;

public class HowToPlay : MonoBehaviour
{
    public string sceneToLoad;

    public void GoToPlay()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
    public void GoBack()
    { 
        //SceneManager.LoadScene(sceneToLoad);
        SceneManager.LoadScene(PlayerPositionData.returnSceneName);
    }
}
