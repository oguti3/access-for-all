using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene loading

public class SceneSwitcher : MonoBehaviour
{
    public string sceneName;

    public string triggeringTag;

    private void OnTriggerEnter(Collider other) // For 3D collisions
    {
        if (other.CompareTag(triggeringTag))
        {
            Debug.Log("ontriggerenter success");
            LoadScene();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) // For 2D collisions
    {
        if (other.CompareTag(triggeringTag))
        {
            Debug.Log("ontriggerenter2d success");
            LoadScene();
        }
    }

    private void LoadScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            // Ensure the scene is added in Build Settings
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Scene name is not set in SceneSwitcher script.");
        }
    }
}
