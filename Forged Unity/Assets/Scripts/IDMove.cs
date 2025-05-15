using UnityEngine;

public class IDMove : MonoBehaviour
{
    // 2D Box Collider Reference
    private BoxCollider2D boxCollider2D;

    // Initial position
    private Vector3 initialPosition;

    // Target X position
    [SerializeField] private float targetX = 100f;

    private void Start()
    {
        // Store the initial position
        initialPosition = transform.position;

        // Get and enable the Box Collider 2D immediately
        boxCollider2D = GetComponent<BoxCollider2D>();
        if (boxCollider2D == null)
        {
            Debug.LogWarning("No BoxCollider2D found on this object.");
        }
    }

    private void OnMouseDown()
    {
        // Move the object to the target X position instantly
        transform.position = new Vector3(targetX, initialPosition.y, initialPosition.z);
    }
}
