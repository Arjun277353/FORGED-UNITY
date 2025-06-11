using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent(typeof(Renderer))]
public class SmoothMoveFadeDimitri : MonoBehaviour
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

        transform.position = targetPosition;

        // Smoothly fade in
        float elapsedTime = 0f;
        Color finalColor = initialColor;
        finalColor.a = 1f;

        while (planeRenderer.material.color.a < 0.99f)
        {
            elapsedTime += Time.deltaTime * fadeSpeed;
            planeRenderer.material.color = Color.Lerp(initialColor, finalColor, elapsedTime);
            yield return null;
        }

        planeRenderer.material.color = finalColor; // Ensure final color is exact

        // Everything is done, now change scene
        SceneManager.LoadScene("Ending News Tuesday");
        Debug.Log("Initiating newspaper scene loading");
    }
}
