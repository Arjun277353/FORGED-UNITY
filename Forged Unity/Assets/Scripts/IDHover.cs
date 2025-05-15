using UnityEngine;
using System.Collections;

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

    // Smooth Damping
    private Vector3 currentPosition;
    private Vector3 positionVelocity;
    private float currentRotation;
    private float rotationVelocity;
    private Vector3 currentScale;
    private Vector3 scaleVelocity;

    // Audio Settings
    [SerializeField] private AudioClip hoverSound;
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private float fadeOutTime = 0.1f;
    private AudioSource audioSource;
    private bool isHovered = false;
    private bool isClicked = false;
    private Coroutine fadeOutCoroutine;

    // Box Collider Reference
    private BoxCollider boxCollider;

    // Smoothing Speed
    [SerializeField] private float smoothTime = 0.05f;

    // Target Sprite and Position
    [SerializeField] private GameObject targetSprite;
    [SerializeField] private Vector3 spriteTargetPosition = new Vector3(0, 5, 6);
    private Vector3 spriteInitialPosition;

    private void Start()
    {
        // Set initial visual state
        transform.position = initialPosition;
        transform.rotation = Quaternion.Euler(initialRotation, 0, 0);
        transform.localScale = initialScale;

        currentPosition = initialPosition;
        currentRotation = initialRotation;
        currentScale = initialScale;

        // Configure audio source
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;

        // Get and enable the Box Collider immediately
        boxCollider = GetComponent<BoxCollider>();
        if (boxCollider != null)
        {
            boxCollider.enabled = true;
        }
        else
        {
            Debug.LogWarning("No BoxCollider found on this object.");
        }

        // Store sprite's initial position
        if (targetSprite != null)
        {
            spriteInitialPosition = targetSprite.transform.position;
        }
    }

    private void Update()
    {
        // Determine target values
        Vector3 targetPos = initialPosition;
        float targetRot = initialRotation;
        Vector3 targetSc = initialScale;

        if (isClicked)
        {
            targetSc = clickScale;
        }
        else if (isHovered)
        {
            targetPos = targetPosition;
            targetRot = targetRotation;
            targetSc = targetScale;
        }

        // Smooth position with enhanced smoothness
        currentPosition = Vector3.SmoothDamp(currentPosition, targetPos, ref positionVelocity, smoothTime, Mathf.Infinity, Time.deltaTime);
        transform.position = currentPosition;

        // Smooth rotation
        currentRotation = Mathf.SmoothDampAngle(currentRotation, targetRot, ref rotationVelocity, smoothTime, Mathf.Infinity, Time.deltaTime);
        transform.rotation = Quaternion.Euler(currentRotation, 0, 0);

        // Smooth scale
        currentScale = Vector3.SmoothDamp(currentScale, targetSc, ref scaleVelocity, smoothTime, Mathf.Infinity, Time.deltaTime);
        transform.localScale = currentScale;
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

        // Disable Box Collider
        if (boxCollider != null)
        {
            boxCollider.enabled = false;
        }

        // Instantly move the sprite
        if (targetSprite != null)
        {
            targetSprite.transform.position = spriteTargetPosition;
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
}
