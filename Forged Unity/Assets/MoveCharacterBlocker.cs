using UnityEngine;

public class MoveCharacterBlockerEvent : MonoBehaviour
{
    [SerializeField] private Transform characterBlocker; 
    [SerializeField] private Vector3 targetPosition = new Vector3(0, 5.6f, 9f); 
    [SerializeField] private float moveSpeed = 2f; 

    
    public void TriggerBlockerMovement()
    {
        if (characterBlocker == null)
        {
            Debug.LogError("Character Blocker is not assigned!");
            return;
        }

        StartCoroutine(MoveBlocker());
    }

    private System.Collections.IEnumerator MoveBlocker()
    {
        Vector3 startPosition = characterBlocker.position;
        float elapsed = 0f;

        while (elapsed < 1f)
        {
            characterBlocker.position = Vector3.Lerp(startPosition, targetPosition, elapsed);
            elapsed += Time.deltaTime * moveSpeed;
            yield return null;
        }

        characterBlocker.position = targetPosition; 
    }
}
