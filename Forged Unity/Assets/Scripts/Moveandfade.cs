using UnityEngine;

public class MoveAndFade : MonoBehaviour
{
    // Target position and speed
    private Vector3 initialPosition;
    private Vector3 targetPosition = new Vector3(0, 5, 8.5f);
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float fadeSpeed = 1f;

    // Opacity Settings
    private MeshRenderer meshRenderer;
    private Material material;
    private Color initialColor;

    private bool hasReachedTarget = false;

    private void Start()
    {
        // Store the initial position
        initialPosition = transform.position;

        // Get the material of the object (ensure it has a material with a shader supporting transparency)
        meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer == null)
        {
            Debug.LogError("No MeshRenderer found on this object.");
            return;
        }

        material = meshRenderer.material;
        initialColor = material.color;
        initialColor.a = 0; // Start with 0 opacity
        material.color = initialColor;
    }

    private void Update()
    {
        if (!hasReachedTarget)
        {
            // Move towards the target position
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Check if target is reached
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                hasReachedTarget = true;
            }
        }
        else
        {
            // Smoothly increase the opacity to 55%
            Color color = material.color;
            color.a = Mathf.MoveTowards(color.a, 0.55f, fadeSpeed * Time.deltaTime);
            material.color = color;
        }
    }
}
