using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class IDHover : MonoBehaviour
{
    // Visual Effect Targets
    private Vector3 initialPosition = new Vector3(0, 0.89f, 7.22f);
    private Vector3 targetPosition = new Vector3(0, 2f, 8.5f);
    private float initialRotation = 40f;
    private float targetRotation = 18f;
    private Vector3 initialScale = Vector3.one * 0.5f;
    private Vector3 targetScale = Vector3.one * 0.7f;
    private Vector3 clickScale = Vector3.one * 0.44f;

    // Audio Settings
    [SerializeField] private AudioClip hoverSound;
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private float fadeOutTime = 0.1f;
    private AudioSource audioSource;
    private bool isHovered = false;
    private bool isClicked = false;
    private bool hasMovedToFinalPosition = false;
    private Coroutine fadeOutCoroutine;

    // Event Trigger (Single Script)
    [SerializeField] private MonoBehaviour scriptToActivate;

    // Box Collider Reference
    private BoxCollider boxCollider;

    private void Start()
    {
        // Set initial visual state
        transform.position = initialPosition;
        transform.rotation = Quaternion.Euler(initialRotation, 0, 0);
        transform.localScale = initialScale;

        // Configure audio source
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;

        // Make sure the target script is initially disabled
        if (scriptToActivate != null) scriptToActivate.enabled = false;

        // Get and ensure the Box Collider is enabled
        boxCollider = GetComponent<BoxCollider>();
        if (boxCollider != null)
        {
            boxCollider.enabled = true; // Enabled by default
            Debug.Log("IDHover: Box Collider found and enabled at start.");
        }
        else
        {
            Debug.LogError("IDHover: No Box Collider found on this object.");
        }
    }

    private void Update()
    {
        if (hasMovedToFinalPosition) return;

        if (isClicked)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, clickScale, Time.deltaTime * 10);
        }
        else if (isHovered)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 5);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(targetRotation, 0, 0), Time.deltaTime * 5);
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * 5);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, initialPosition, Time.deltaTime * 5);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(initialRotation, 0, 0), Time.deltaTime * 5);
            transform.localScale = Vector3.Lerp(transform.localScale, initialScale, Time.deltaTime * 5);
        }
    }

    private void OnMouseEnter()
    {
        if (boxCollider == null || !boxCollider.enabled) return;

        isHovered = true;

        if (fadeOutCoroutine != null)
        {
            StopCoroutine(fadeOutCoroutine);
            fadeOutCoroutine = null;
        }

        if (hoverSound != null)
        {
            audioSource.volume = 1f;
            audioSource.PlayOneShot(hoverSound);
        }
    }

    private void OnMouseExit()
    {
        isHovered = false;
        isClicked = false;

        if (hoverSound != null && audioSource.isPlaying)
        {
            if (fadeOutCoroutine != null)
            {
                StopCoroutine(fadeOutCoroutine);
            }
            fadeOutCoroutine = StartCoroutine(FadeOutSound());
        }
    }

    private void OnMouseDown()
    {
        if (boxCollider == null || !boxCollider.enabled) return;

        isClicked = true;
        if (clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }

    private void OnMouseUp()
    {
        isClicked = false;

        // Instantly move to Y = 20 and stay there
        transform.position = new Vector3(transform.position.x, 20, transform.position.z);
        hasMovedToFinalPosition = true;

        // Activate the specified script if set
        if (scriptToActivate != null)
        {
            scriptToActivate.enabled = true;
            Debug.Log("IDHover: Script activated.");
        }
    }

    private IEnumerator FadeOutSound()
    {
        float startVolume = audioSource.volume;
        float elapsedTime = 0f;

        while (elapsedTime < fadeOutTime)
        {
            elapsedTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, elapsedTime / fadeOutTime);
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
        fadeOutCoroutine = null;
    }

    
    public void EnableCollider()
    {
        if (boxCollider == null)
        {
            boxCollider = GetComponent<BoxCollider>();
            if (boxCollider == null)
            {
                Debug.LogError("IDHover: Attempted to enable collider, but it does not exist.");
                return;
            }
        }

        boxCollider.enabled = true;
        Debug.Log("IDHover: Box Collider is already enabled or has been enabled.");
    }
}
