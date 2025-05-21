using UnityEngine;
using UnityEngine.UI;

public class MoveOnAllChecked : MonoBehaviour
{
    [Header("Checkbox Toggles")]
    public Toggle[] checkboxes;

    [Header("Target Object to Move")]
    public GameObject targetObject;

    [Header("Target Position")]
    public Vector3 targetPosition = new Vector3(2f, 1.5f, 8f);

    private bool hasMoved = false;

    private void Update()
    {
        if (!hasMoved && AllChecked())
        {
            if (targetObject != null)
            {
                targetObject.transform.position = targetPosition;
                hasMoved = true; // Prevent repeating
            }
        }
    }

    private bool AllChecked()
    {
        foreach (Toggle toggle in checkboxes)
        {
            if (toggle == null || !toggle.isOn)
                return false;
        }
        return true;
    }
}
