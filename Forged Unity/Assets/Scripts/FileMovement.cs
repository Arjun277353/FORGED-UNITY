using UnityEngine;

public class InstantMoveAndFadeIn : MonoBehaviour
{
    [SerializeField] private float targetY = 5f; 
    [SerializeField] private float fadeDuration = 1f; 
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

        
        Color startColor = spriteRenderer.color;
        startColor.a = 0f;
        spriteRenderer.color = startColor;
    }

    private void Update()
    {
        if (!hasMoved)
        {
            
            transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
            hasMoved = true;

            
            StartCoroutine(FadeIn());
        }
    }

    private System.Collections.IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        Color startColor = spriteRenderer.color;
        Color targetColor = startColor;
        targetColor.a = 1f; 

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            spriteRenderer.color = Color.Lerp(startColor, targetColor, elapsedTime / fadeDuration);
            yield return null;
        }

        spriteRenderer.color = targetColor; 
        this.enabled = false; 
    }
}
