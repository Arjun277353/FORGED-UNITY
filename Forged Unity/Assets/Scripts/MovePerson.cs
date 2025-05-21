using UnityEngine;

public class MoveObjectOn2DBoxClick : MonoBehaviour
{
    [Header("Object to Move")]
    public Transform objectToMove;

    [Header("Target X Position")]
    public float targetX = 10.1f;

    private void OnMouseDown()
    {
        if (objectToMove != null)
        {
            Vector3 pos = objectToMove.position;
            objectToMove.position = new Vector3(targetX, pos.y, pos.z);
        }
    }
}
