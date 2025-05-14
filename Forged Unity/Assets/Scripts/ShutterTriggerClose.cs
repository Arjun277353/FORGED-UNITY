using UnityEngine;

public class TriggerAnimationTest: MonoBehaviour
{
    [SerializeField] private Animator shutterAnimator;
    [SerializeField] private GameObject triggerButton; 

    private bool isOpened = false;
    private bool dialogueFinished = false;

    private void OnMouseDown()
    {
        Debug.Log("Trigger Pressed");

        if (shutterAnimator == null)
        {
            Debug.LogError("Shutter Animator is missing!");
            return;
        }

        
        if (!isOpened && !dialogueFinished)
        {
            Debug.Log("Opening Shutter");
            shutterAnimator.SetBool("IsOpen", true);
            isOpened = true;

            
            if (triggerButton != null)
            {
                triggerButton.GetComponent<Collider2D>().enabled = false;
                Debug.Log("Trigger Disabled");
            }

        }

       
        else if (isOpened && dialogueFinished)
        {
            Debug.Log("Closing Shutter");
            shutterAnimator.SetBool("IsOpen", false);
            isOpened = false;

            
            triggerButton.GetComponent<Collider2D>().enabled = false;
        }
    }

    
    public void OnDialogueFinished()
    {
        Debug.Log("Dialogue Finished");

        dialogueFinished = true;

        
        if (triggerButton != null)
        {
            triggerButton.GetComponent<Collider2D>().enabled = true;
            Debug.Log("Trigger Re-enabled for Closing");
        }
    }
}
