using UnityEngine;

public class HitboxCheck : MonoBehaviour
{
    public BoxCollider2D checkbox1;
    public BoxCollider2D checkbox2;
    public BoxCollider2D checkbox3;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Draw debug ray in Scene view
            Debug.DrawRay(mouseWorldPos, Vector3.forward * 0.1f, Color.yellow, 1f);

            // Check all hit colliders at this point
            Collider2D[] hits = Physics2D.OverlapPointAll(mouseWorldPos);

            if (hits.Length == 0)
            {
                Debug.Log("No collider hit");
            }
            else
            {
                foreach (Collider2D col in hits)
                {
                    Debug.Log("Hit: " + col.name);
                }
            }

            // Optional direct checks (if still needed for logic flow)
            if (checkbox1.OverlapPoint(mouseWorldPos))
                Debug.Log("Drawing inside checkbox 1");
            else if (checkbox2.OverlapPoint(mouseWorldPos))
                Debug.Log("Drawing inside checkbox 2");
            else if (checkbox3.OverlapPoint(mouseWorldPos))
                Debug.Log("Drawing inside checkbox 3");
            else
                Debug.Log("Not inside any checkbox");
        }
    }

    void OnDrawGizmos()
    {
        if (checkbox1 != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(checkbox1.bounds.center, checkbox1.bounds.size);
        }

        if (checkbox2 != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(checkbox2.bounds.center, checkbox2.bounds.size);
        }

        if (checkbox3 != null)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawWireCube(checkbox3.bounds.center, checkbox3.bounds.size);
        }
    }
}
