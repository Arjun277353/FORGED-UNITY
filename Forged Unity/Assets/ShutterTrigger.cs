using UnityEngine;

public class TriggerAnimation : MonoBehaviour
{
    [SerializeField] private Animator shutterAnimator; // Assign the shutter's Animator in the Inspector
    [SerializeField] private GameObject triggerButton; // Assign the trigger button object
    private bool hasOpened = false;

    private void OnMouseDown()
    {
        if (hasOpened || shutterAnimator == null) return;

        shutterAnimator.SetBool("PlayOpen", true);
        hasOpened = true;

        // Disable button (make it uninteractable)
        if (triggerButton != null)
        {
            triggerButton.GetComponent<Collider2D>().enabled = false;
        }
    }
}
