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

        // Enable the HoverEffect script using its exact name
        HoverEffect hoverScript = document.GetComponent<HoverEffect>();
        if (hoverScript != null)
        {
            hoverScript.enabled = true;
            Debug.Log("HoverEffect script enabled on Document.");
        }
        else
        {
            Debug.LogError("HoverEffect script not found on Document!");
        }
    }
}
