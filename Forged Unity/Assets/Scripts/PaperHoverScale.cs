using UnityEngine;

public class PaperHoverScale : MonoBehaviour
{
    private Vector3 originalScale;
    private Vector3 targetScale;
    private Vector3 scaleVelocity = Vector3.zero;
    private Vector3 positionVelocity = Vector3.zero;
    private Vector3 originalPosition;

    public float hoverScale = 1.2f;
    public float clickScale = 0.8f;
    public float smoothTime = 0.1f;
    public float moveDistance = 2f; // Distance to move to the left

    private static PaperHoverScale[] allHoverObjects;

    void Start()
    {
        originalScale = transform.localScale;
        originalPosition = transform.position;
        targetScale = originalScale;

        // Find all objects with this script
        if (allHoverObjects == null)
        {
            allHoverObjects = FindObjectsOfType<PaperHoverScale>();
        }
    }

    void Update()
    {
        // Smoothly transition scale
        transform.localScale = Vector3.SmoothDamp(transform.localScale, targetScale, ref scaleVelocity, smoothTime);
    }

    void OnMouseEnter()
    {
        targetScale = originalScale * hoverScale;
    }

    void OnMouseExit()
    {
        targetScale = originalScale;
    }

    void OnMouseDown()
    {
        foreach (var obj in allHoverObjects)
        {
            obj.targetScale = obj.originalScale * clickScale;
            obj.MoveObjectSmooth();
        }
    }

    void OnMouseUp()
    {
        foreach (var obj in allHoverObjects)
        {
            obj.targetScale = obj.originalScale; // Return to original scale (1)
        }
    }

    // Smoothly move each object to the left using SmoothDamp
    private void MoveObjectSmooth()
    {
        Vector3 targetPosition = originalPosition + new Vector3(-moveDistance, 0, 0);
        StartCoroutine(SmoothMoveToPosition(targetPosition));
    }

    // Smooth movement coroutine
    private System.Collections.IEnumerator SmoothMoveToPosition(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref positionVelocity, smoothTime);
            yield return null;
        }
        transform.position = targetPosition;
    }
}
