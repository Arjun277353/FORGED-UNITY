using UnityEngine;

public class MoveObjectsOnSecondClick : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsToMove;

    private int clickCount = 0;
    private bool isPressed = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (GetComponent<BoxCollider2D>().OverlapPoint(mouseWorldPos))
            {
                isPressed = true;
            }
        }

        if (isPressed && Input.GetMouseButtonUp(0))
        {
            clickCount++;
            isPressed = false;

            if (clickCount == 2)
            {
                foreach (GameObject obj in objectsToMove)
                {
                    if (obj != null)
                    {
                        Vector3 pos = obj.transform.position;
                        obj.transform.position = new Vector3(100f, pos.y, pos.z);
                    }
                }

                // Optional: reset click count if you want it to work again
                // clickCount = 0;
            }
        }
    }
}
