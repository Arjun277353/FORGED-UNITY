using UnityEngine;

public class HoverEffect : MonoBehaviour
{
    // Position, Rotation, and Scale Targets
    private Vector3 initialPosition = new Vector3(0, 0.89f, 8.34f);
    private Vector3 targetPosition = new Vector3(0, 1.8f, 8.34f);

    private float initialRotation = 92.67f;
    private float targetRotation = 25f;

    private Vector3 initialScale = Vector3.one * 0.5f;
    private Vector3 targetScale = Vector3.one * 0.6f;

    private bool isHovered = false;

    private void Start()
    {
        // Set initial state
        transform.position = initialPosition;
        transform.rotation = Quaternion.Euler(initialRotation, 0, 0);
        transform.localScale = initialScale;
    }

    private void Update()
    {
        if (isHovered)
        {
            // Smooth Transition to Target
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 5);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(targetRotation, 0, 0), Time.deltaTime * 5);
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * 5);
        }
        else
        {
            // Smooth Return to Initial
            transform.position = Vector3.Lerp(transform.position, initialPosition, Time.deltaTime * 5);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(initialRotation, 0, 0), Time.deltaTime * 5);
            transform.localScale = Vector3.Lerp(transform.localScale, initialScale, Time.deltaTime * 5);
        }
    }

    private void OnMouseEnter()
    {
        isHovered = true;
    }

    private void OnMouseExit()
    {
        isHovered = false;
    }
}