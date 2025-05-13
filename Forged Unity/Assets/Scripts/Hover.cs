using UnityEngine;
using System.Collections;

public class HoverEffect : MonoBehaviour
{
    // Visual Effect Targets
    private Vector3 initialPosition = new Vector3(0, 0.89f, 7.22f);
    private Vector3 targetPosition = new Vector3(0, 2f, 8.5f); // Updated target position
    private float initialRotation = 40f;
    private float targetRotation = 18f; // Updated target rotation (X axis)
    private Vector3 initialScale = Vector3.one * 0.5f;
    private Vector3 targetScale = Vector3.one * 0.7f; // Updated target scale
    private Vector3 clickScale = Vector3.one * 0.44f;

    // Audio Settings
    [SerializeField] private AudioClip hoverSound;
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private float fadeOutTime = 0.1f;
    private AudioSource audioSource;
    private bool isHovered = false;
    private bool isClicked = false;
    private Coroutine fadeOutCoroutine;

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
    }

    private void Update()
    {
        // Handle click and hold scaling
        if (isClicked && Input.GetMouseButton(0))
        {
            transform.localScale = Vector3.Lerp(transform.localScale, clickScale, Time.deltaTime * 10);
        }
        else if (isHovered)
        {
            // Smooth hover transition with new target values
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 5);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(targetRotation, 0, 0), Time.deltaTime * 5);
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * 5);
        }
        else
        {
            // Smooth return to initial
            transform.position = Vector3.Lerp(transform.position, initialPosition, Time.deltaTime * 5);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(initialRotation, 0, 0), Time.deltaTime * 5);
            transform.localScale = Vector3.Lerp(transform.localScale, initialScale, Time.deltaTime * 5);
        }
    }

    // Rest of the script remains unchanged...
    private void OnMouseEnter()
    {
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
        isClicked = true;
        if (clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }

    private void OnMouseUp()
    {
        isClicked = false;
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
}