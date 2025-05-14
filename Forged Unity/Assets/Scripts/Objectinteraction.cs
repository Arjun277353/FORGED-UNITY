using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public GameObject relatedObject; // Assign in inspector (A->1, B->2, etc.)
    public Vector3 targetPosition = new Vector3(0, 5, 8); // Target position for related objects

    private Vector3 originalScale;
    private Vector3 originalRelatedObjectPosition;
    private bool isClicked = false;

    void Start()
    {
        originalScale = transform.localScale;
        if (relatedObject != null)
        {
            originalRelatedObjectPosition = relatedObject.transform.position;
        }
    }

    // Hover enter - scale up to 1.2
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isClicked)
        {
            transform.localScale = originalScale * 1.2f;
        }
    }

    // Hover exit - return to normal scale
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isClicked)
        {
            transform.localScale = originalScale;
        }
    }

    // Mouse down - scale down to 0.8
    public void OnPointerDown(PointerEventData eventData)
    {
        isClicked = true;
        transform.localScale = originalScale * 0.8f;
    }

    // Mouse up - return to normal scale and move objects
    public void OnPointerUp(PointerEventData eventData)
    {
        isClicked = false;
        transform.localScale = originalScale;

        // Move all A-D objects to the left
        MoveAllObjectsLeft();

        // Move the related object to target position
        if (relatedObject != null)
        {
            relatedObject.transform.position = targetPosition;
        }
    }

    private void MoveAllObjectsLeft()
    {
        // Find all objects with ObjectInteraction script (A-D)
        ObjectInteraction[] allObjects = FindObjectsOfType<ObjectInteraction>();

        foreach (ObjectInteraction obj in allObjects)
        {
            // Move each object to the left (adjust -5f to your desired left position)
            obj.transform.position += Vector3.left * 5f;

            // Reset their related objects to original positions (except the clicked one)
            if (obj != this && obj.relatedObject != null)
            {
                obj.relatedObject.transform.position = obj.originalRelatedObjectPosition;
            }
        }
    }
}