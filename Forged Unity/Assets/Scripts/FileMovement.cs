using UnityEngine;

public class InstantMoveAndFadeIn : MonoBehaviour
{
    [SerializeField] private float targetY = 5f; // Target Y position
    [SerializeField] private float fadeDuration = 1f; // Duration for fade-in effect
    private SpriteRenderer spriteRenderer;
    private bool hasMoved = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("No SpriteRenderer attached to this object.");
            return;
        }

        // Set initial opacity to 0 (completely transparent)
        Color startColor = spriteRenderer.color;
        startColor.a = 0f;
        spriteRenderer.color = startColor;
    }

    private void Update()
    {
        if (!hasMoved)
        {
            // Instantly move the object to target Y position
            transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
            hasMoved = true;

            // Start reverse fade-in
            StartCoroutine(FadeIn());
        }
    }

    private System.Collections.IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        Color startColor = spriteRenderer.color;
        Color targetColor = startColor;
        targetColor.a = 1f; // Fully opaque

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            spriteRenderer.color = Color.Lerp(startColor, targetColor, elapsedTime / fadeDuration);
            yield return null;
        }

        spriteRenderer.color = targetColor; // Ensure it's fully visible at the end
        this.enabled = false; // Disable the script after fading in
    }
}
