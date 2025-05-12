using UnityEngine;

public class TriggerAnimationClose : MonoBehaviour
{
    [SerializeField] private Animator shutterAnimator; // Assign the shutter's Animator in the Inspector
    [SerializeField] private GameObject triggerButton; // Assign the trigger button object (with 2D Box Collider)
    private bool hasClosed = false;

    private void OnMouseDown()
    {
        Debug.Log("Trigger Pressed");

        if (hasClosed || shutterAnimator == null)
        {
            Debug.LogError("Already closed or shutterAnimator is missing!");
            return;
        }

        Debug.Log("Closing Shutter Directly");
        shutterAnimator.Play("Open"); // Directly plays the Open animation
        hasClosed = true;

        // Disable button (make it uninteractable)
        if (triggerButton != null)
        {
            triggerButton.GetComponent<Collider2D>().enabled = false;
            Debug.Log("Trigger Disabled");
        }
    }
}
