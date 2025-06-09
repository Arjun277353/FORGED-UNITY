using UnityEngine;

public class FadeOutTrigger : MonoBehaviour
{
  [Header("Smooth Move and Fade Settings")]
    [SerializeField] private SmoothMoveFade2 moveFadeScript;

    void Start()
    {
        if (moveFadeScript != null)
        {
            moveFadeScript.enabled = false;
        }
    }
    public void TriggerFadeOut()
    {
        if (moveFadeScript != null)
        {
            moveFadeScript.enabled = true;
            Debug.Log("SmoothMoveFade script activated from dialogue.");
        }
        else
        {
            Debug.LogWarning("MoveFade script is not assigned.");
        }
    }
}
