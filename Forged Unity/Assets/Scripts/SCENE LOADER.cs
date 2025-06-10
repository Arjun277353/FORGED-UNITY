using UnityEngine;
using UnityEngine.SceneManagement;

public class SequentialSceneLoader : MonoBehaviour
{
    public bool autoLoadNext = false;
    public float delayBeforeNextScene = 1f;

    /*
    void Start()
    {
        if (autoLoadNext)
        {
            Invoke("LoadNextScene", delayBeforeNextScene);
        }
    }
    */

    public void EnableAutoLoad()
    {
        Debug.Log("AutoLoadNext manually triggered.");
        autoLoadNext = true;
        Invoke("LoadNextScene", delayBeforeNextScene);
    }

    public void LoadNextScene()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = currentIndex + 1;

        if (nextIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextIndex);
        }
        else
        {
            Debug.LogWarning("No more scenes to load.");
        }
    }
}
