using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string nextSceneName;
    public float timeToWait=5f;

    private void Start()
    {
        // Wait for 5 seconds before loading the next scene
        Invoke("LoadNextScene", timeToWait);
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
