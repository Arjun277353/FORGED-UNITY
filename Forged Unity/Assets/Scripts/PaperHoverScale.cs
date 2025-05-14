using UnityEngine;

public class PaperHoverScale : MonoBehaviour
{
    private Vector3 originalScale;
    public float hoverScale = 1.2f;
    public float clickScale = 0.8f;
    public float smoothTime = 0.1f;

    private Vector3 velocity = Vector3.zero;
    private Vector3 targetScale;

    void Start()
    {
        originalScale = transform.localScale;
        targetScale = originalScale;
    }

    void Update()
    {
        transform.localScale = Vector3.SmoothDamp(transform.localScale, targetScale, ref velocity, smoothTime);
    }

    void OnMouseEnter()
    {
        targetScale = originalScale * hoverScale;
    }

    void OnMouseExit()
    {
        targetScale = originalScale;
    }

    void OnMouseDown()
    {
        targetScale = originalScale * clickScale;
    }

    void OnMouseUp()
    {
        targetScale = originalScale * hoverScale;
    }
}
