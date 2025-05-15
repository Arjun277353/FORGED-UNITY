using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class HoverEffect : MonoBehaviour
{
    // Visual Effect Targets
    private Vector3 initialScale = Vector3.one;
    private Vector3 hoverScale = Vector3.one * 1.2f;
    private Vector3 clickScale = Vector3.one * 0.8f;

    // Audio Settings
    [SerializeField] private AudioClip hoverSound;
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private float fadeOutTime = 0.1f;
    private AudioSource audioSource;
    private bool isHovered = false;
    private bool isClicked = false;
    private Coroutine fadeOutCoroutine;

    // Event Triggers (Up to 5 scripts)
    [SerializeField] private MonoBehaviour scriptToActivate1;
    [SerializeField] private MonoBehaviour scriptToActivate2;
    [SerializeField] private MonoBehaviour scriptToActivate3;
    [SerializeField] private MonoBehaviour scriptToActivate4;
    [SerializeField] private MonoBehaviour scriptToActivate5;

    // 2D Box Collider Reference
    private BoxCollider2D boxCollider2D;

    private void Start()
    {
        // Set initial visual state
        transform.localScale = initialScale;

        // Configure audio source
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;

        // Make sure the target scripts are initially disabled
        DisableScripts();

        // Get and enable the 2D Box Collider immediately
        boxCollider2D = GetComponent<BoxCollider2D>();
        if (boxCollider2D != null)
        {
            boxCollider2D.enabled = true;
        }
        else
        {
            Debug.LogWarning("No 2D BoxCollider found on this object.");
        }
    }

    private void Update()
    {
        // Smoothly adjust the scale based on hover and click state
        if (isClicked)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, clickScale, Time.deltaTime * 10);
        }
        else if (isHovered)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, hoverScale, Time.deltaTime * 5);
        }
        else
        {
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

        // Disable the 2D BoxCollider immediately
        if (boxCollider2D != null)
        {
            boxCollider2D.enabled = false;
        }
    }

    private void OnMouseUp()
    {
        isClicked = false;
        isHovered = true; // Briefly back to hover scale
        StartCoroutine(ResetToNormalScale());

        // Activate the specified scripts
        EnableScripts();
    }

    private IEnumerator ResetToNormalScale()
    {
        yield return new WaitForSeconds(0.1f); // Small delay for smooth transition
        isHovered = false; // This will smoothly scale it back to 1
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
