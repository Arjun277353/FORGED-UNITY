using UnityEngine;

public class CloseEnable : MonoBehaviour
{
    public Animator targetAnimator; // The animator on CloseShutter
    private Collider2D triggerCollider;

    void Start()
    {
        triggerCollider = GetComponent<Collider2D>();
        if (triggerCollider != null)
        {
            triggerCollider.enabled = false; // Disable by default
        }
        else
        {
            Debug.LogWarning("No Collider2D found on this object.");
        }
    }

    // Method to enable the trigger (called from Dialogue Editor)
    public void EnableTrigger()
    {
        if (triggerCollider != null)
        {
            triggerCollider.enabled = true;
            Debug.Log("CloseTrigger enabled.");
        }
    }

    private void OnMouseDown()
    {
        if (triggerCollider != null && triggerCollider.enabled)
        {
            if (targetAnimator != null)
            {
                targetAnimator.SetTrigger("Close");
                Debug.Log($"Animation 'Close' triggered on {targetAnimator.gameObject.name}.");
            }
            else
            {
                Debug.LogWarning("Target Animator is not assigned.");
            }
        }
    }
}
