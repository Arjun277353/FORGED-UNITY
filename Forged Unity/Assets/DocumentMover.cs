using UnityEngine;
using UnityEngine.Events;

public class MoveDocumentEvent : MonoBehaviour
{
    [SerializeField] private Transform document; 
    [SerializeField] private Vector3 targetPosition = new Vector3(0, 0.89f, 8.34f); 

    [Header("Event Triggers")]
    public UnityEvent onDocumentMoved; 

    public void TriggerDocumentMovement()
    {
        if (document == null)
        {
            Debug.LogError("Document is not assigned!");
            return;
        }

        document.position = targetPosition; 
        onDocumentMoved?.Invoke(); 
    }
}
