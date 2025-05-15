using UnityEngine;
using System.Collections;

public class IDMove : MonoBehaviour
{
    // 2D Box Collider Reference
    private BoxCollider2D boxCollider2D;

    // Initial and target positions
    private Vector3 initialPosition;
    [SerializeField] private float targetXInitial = 0f;
    [SerializeField] private float targetXFinal = 100f;

    // Delay for enabling Box Collider
    [SerializeField] private float delaySeconds = 2f;

    // Script to activate after clicking
    [SerializeField] private MonoBehaviour scriptToActivate;

    private void Start()
    {
        // Store the initial position
        initialPosition = transform.position;

        // Get and disable the Box Collider 2D immediately
        boxCollider2D = GetComponent<BoxCollider2D>();
        if (boxCollider2D != null)
        {
            boxCollider2D.enabled = false; // Ensure it's disabled at start
            Debug.Log("IDMove: 2D Box Collider found and disabled at start.");
        }
        else
        {
            Debug.LogWarning("IDMove: No BoxCollider2D found on this object.");
        }

        // Instantly move to the initial target position (x = 0)
        transform.position = new Vector3(targetXInitial, initialPosition.y, initialPosition.z);
        Debug.Log("IDMove: Instantly moved to target X = " + targetXInitial);

        // Start the delayed collider activation
        StartCoroutine(EnableColliderAfterDelay());
    }

    private IEnumerator EnableColliderAfterDelay()
    {
        yield return new WaitForSeconds(delaySeconds);

        // Enable the Box Collider 2D after delay
        if (boxCollider2D != null)
        {
            boxCollider2D.enabled = true;
            Debug.Log("IDMove: 2D Box Collider enabled after delay.");
        }
    }

    private void OnMouseDown()
    {
        // If the collider is enabled, move to the final target position (x = 100)
        if (boxCollider2D != null && boxCollider2D.enabled)
        {
            transform.position = new Vector3(targetXFinal, initialPosition.y, initialPosition.z);
            Debug.Log("IDMove: Moved to final target X = " + targetXFinal);

            // Activate the specified script after the move
            if (scriptToActivate != null)
            {
                scriptToActivate.enabled = true;
                Debug.Log("IDMove: Script " + scriptToActivate.GetType().Name + " activated.");
            }
        }
    }
}
