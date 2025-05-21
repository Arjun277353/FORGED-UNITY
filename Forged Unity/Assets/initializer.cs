using UnityEngine;

public class SceneBInitializer : MonoBehaviour
{
    [SerializeField] private MonoBehaviour movementScript; // Reference to the disabled script

    void Start()
    {
        if (SceneState.shouldMoveObject)
        {
            movementScript.enabled = true;
            Debug.Log("Movement script enabled in Scene B.");
            SceneState.shouldMoveObject = false; // Optional: reset flag
        }
    }
}
