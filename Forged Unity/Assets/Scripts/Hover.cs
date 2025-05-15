using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class HoverEffect : MonoBehaviour
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

    // Event Triggers (Up to 5 scripts)
    [SerializeField] private MonoBehaviour scriptToActivate1;
    [SerializeField] private MonoBehaviour scriptToActivate2;
    [SerializeField] private MonoBehaviour scriptToActivate3;
    [SerializeField] private MonoBehaviour scriptToActivate4;
    [SerializeField] private MonoBehaviour scriptToActivate5;

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

        // Make sure the target scripts are initially disabled
        DisableScripts();

        // Get and enable the Box Collider immediately
        boxCollider = GetComponent<BoxCollider>();
        if (boxCollider != null)
        {
            boxCollider.enabled = true; // Enabled immediately
        }
    }

    private void Update()
    {
        if (hasMovedToFinalPosition)
            return; // Stop further updates once it has moved to Y = 20

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

        // Instantly move to Y = 20 and stay there
        transform.position = new Vector3(transform.position.x, 20, transform.position.z);
        hasMovedToFinalPosition = true;

        // Activate the specified scripts
        EnableScripts();
    }

    private void EnableScripts()
    {
        if (scriptToActivate1 != null) scriptToActivate1.enabled = true;
        if (scriptToActivate2 != null) scriptToActivate2.enabled = true;
        if (scriptToActivate3 != null) scriptToActivate3.enabled = true;
        if (scriptToActivate4 != null) scriptToActivate4.enabled = true;
        if (scriptToActivate5 != null) scriptToActivate5.enabled = true;
    }

    private void DisableScripts()
    {
        if (scriptToActivate1 != null) scriptToActivate1.enabled = false;
        if (scriptToActivate2 != null) scriptToActivate2.enabled = false;
        if (scriptToActivate3 != null) scriptToActivate3.enabled = false;
        if (scriptToActivate4 != null) scriptToActivate4.enabled = false;
        if (scriptToActivate5 != null) scriptToActivate5.enabled = false;
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
