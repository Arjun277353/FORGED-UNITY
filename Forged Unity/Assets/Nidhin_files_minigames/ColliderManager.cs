using UnityEngine;

public class CheckboxDetector : MonoBehaviour
{
    public BoxCollider2D checkbox1;
    public BoxCollider2D checkbox2;
    public BoxCollider2D signatureBox;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (checkbox1.OverlapPoint(mouseWorldPos))
            {
                Debug.Log("Checkbox 1 clicked");
            }
            else if (checkbox2.OverlapPoint(mouseWorldPos))
            {
                Debug.Log("Checkbox 2 clicked");
            }
            else if (signatureBox.OverlapPoint(mouseWorldPos))
            {
                Debug.Log("Signature box clicked");
            }
            else
            {
                Debug.Log("Clicked outside all boxes");
            }
        }
    }
}