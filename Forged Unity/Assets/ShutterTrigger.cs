using UnityEngine;

public class TriggerAnimation : MonoBehaviour
{
    [SerializeField] private Animator shutterAnimator; // Assign the shutter's Animator in the Inspector
    [SerializeField] private GameObject triggerButton; // Assign the trigger button object (with 2D Box Collider)
    private bool hasOpened = false;

    private void OnMouseDown()
    {
        Debug.Log("Trigger Pressed");

        if (hasOpened || shutterAnimator == null)
        {
            Debug.LogError("Already opened or shutterAnimator is missing!");
            return;
        }

        Debug.Log("Opening Shutter Directly");
        shutterAnimator.Play("Open"); // Directly plays the Open animation
        hasOpened = true;

        // Disable button (make it uninteractable)
        if (triggerButton != null)
        {
            triggerButton.GetComponent<Collider2D>().enabled = false;
            Debug.Log("Trigger Disabled");
        }
    }
}
