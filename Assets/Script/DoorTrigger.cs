using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTrigger : MonoBehaviour
{
    public string sceneToLoad;
    public string spawnPointID;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            Debug.Log("Player collided with the door!");
            PlayerPositionData.nextSpawnPointID = spawnPointID; // Set the spawn point ID
            SceneManager.LoadScene(sceneToLoad); 
        }
    }
}

