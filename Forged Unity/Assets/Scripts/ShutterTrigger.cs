using UnityEngine;

public class TriggerAnimation : MonoBehaviour
{
    [SerializeField] private Animator shutterAnimator;
    [SerializeField] private GameObject triggerButton;
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
        shutterAnimator.SetBool("IsOpen", true);
        hasOpened = true;


        if (triggerButton != null)
        {
            triggerButton.GetComponent<Collider2D>().enabled = false;
            Debug.Log("Trigger Disabled");
        }
    }
}