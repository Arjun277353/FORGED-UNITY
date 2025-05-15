using UnityEngine;
using System.Collections; // Ensure this is included

[RequireComponent(typeof(Renderer))]
public class SmoothMoveFade : MonoBehaviour
{
    [SerializeField] private Vector3 targetPosition = new Vector3(0, 5, 0);
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float fadeSpeed = 1f;

    private Vector3 initialPosition;
    private Renderer planeRenderer;
    private Color initialColor;

    private void Start()
    {
        initialPosition = transform.position;
        planeRenderer = GetComponent<Renderer>();

        // Set initial opacity to 0
        initialColor = planeRenderer.material.color;
        initialColor.a = 0f;
        planeRenderer.material.color = initialColor;

        StartCoroutine(MoveAndFade());
    }

    private IEnumerator MoveAndFade()
    {
        // Smoothly move to target position
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // Ensure it reaches the exact target
        transform.position = targetPosition;

        // Start fade-in after reaching the target
        float elapsedTime = 0f;
        Color finalColor = initialColor;
        finalColor.a = 1f;

        while (planeRenderer.material.color.a < 1f)
        {
            elapsedTime += Time.deltaTime * fadeSpeed;
            planeRenderer.material.color = Color.Lerp(initialColor, finalColor, elapsedTime);
            yield return null;
        }
    }
}
