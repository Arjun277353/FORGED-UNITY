using UnityEngine;

public class MoveToPosition : MonoBehaviour
{
    [SerializeField] private Vector3 targetPosition = new Vector3(0f, 4.68f, 7.99f);

    void Start()
    {
        transform.position = targetPosition;
    }
}
