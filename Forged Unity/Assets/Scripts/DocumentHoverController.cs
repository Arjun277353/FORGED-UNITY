using UnityEngine;

public class DocumentHoverController : MonoBehaviour
{
    public void EnableHoverOnDocument()
    {
        // Find the Document GameObject by name
        GameObject document = GameObject.Find("Document");

        if (document == null)
        {
            Debug.LogError("Document GameObject not found in the scene!");
            return;
        }

        // Try enabling IDHover script first
        IDHover idHoverScript = document.GetComponent<IDHover>();
        if (idHoverScript != null)
        {
            idHoverScript.enabled = true;
            Debug.Log("IDHover script enabled on Document.");
            return;
        }

        // If IDHover is not found, try enabling HoverEffect (for compatibility)
        HoverEffect hoverScript = document.GetComponent<HoverEffect>();
        if (hoverScript != null)
        {
            hoverScript.enabled = true;
            Debug.Log("HoverEffect script enabled on Document.");
            return;
        }

        // If neither script is found, log an error
        Debug.LogError("Neither IDHover nor HoverEffect script found on Document!");
    }
}
