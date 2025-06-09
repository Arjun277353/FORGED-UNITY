using UnityEngine;

public class CloseEnable : MonoBehaviour
{
    public Animator targetAnimator; 
    private Collider2D triggerCollider;

    [Header("Smooth Move and Fade Settings")]
    [SerializeField] private SmoothMoveFade moveFadeScript; 

    void Start()
    {
        triggerCollider = GetComponent<Collider2D>();
        if (triggerCollider != null)
        {
            triggerCollider.enabled = false; 
        }
        else
        {
            Debug.LogWarning("No Collider2D found on this object.");
        }

        
        if (moveFadeScript != null)
        {
            moveFadeScript.enabled = false;
        }
    }

    private void Update()
    {
        if (moveFadeScript != null)
        {
            moveFadeScript.enabled = true;
            Debug.Log("SmoothMoveFade script activated.");
        }
        else
        {
            Debug.LogWarning("MoveFade script is not assigned.");
        }
    }

    /* 
    //Im not deleting anything in case yall wanna use the button in the future
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
    }*/
}