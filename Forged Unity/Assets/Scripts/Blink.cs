using UnityEngine;

public class BlinkingSprite : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    private float timer = 0f;
    public float blinkInterval = 3f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= blinkInterval)
        {
            timer = 0f;
            spriteRenderer.enabled = !spriteRenderer.enabled;
        }
    }
}