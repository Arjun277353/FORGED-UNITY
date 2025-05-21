using UnityEngine;

public class ClickReleaseToMoveEnableAndHideSelf : MonoBehaviour
{
    [Header("Target Object to Move and Enable")]
    public GameObject targetObject;

    [Header("Target X Position for Target Object")]
    public float targetX = 0f;

    [Header("Self Move X Position After Release")]
    public float selfTargetX = -20f;

    private bool isHovering = false;

    private void OnMouseEnter()
    {
        isHovering = true;
    }

    private void OnMouseExit()
    {
        isHovering = false;
    }

    private void Update()
    {
        if (isHovering && Input.GetMouseButtonUp(0))
        {
            // Move and enable the target object
            if (targetObject != null)
            {
                targetObject.SetActive(true);
                Vector3 currentPos = targetObject.transform.position;
                targetObject.transform.position = new Vector3(targetX, currentPos.y, currentPos.z);
            }

            // Move this object to x = -20
            Vector3 myPos = transform.position;
            transform.position = new Vector3(selfTargetX, myPos.y, myPos.z);

            // Prevent it from repeating multiple times
            isHovering = false;
        }
    }
}
