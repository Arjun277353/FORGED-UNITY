using UnityEngine;
using System.Collections;

public class HoverEffect : MonoBehaviour
{
    
    private Vector3 initialScale = Vector3.one;
    private Vector3 hoverScale = Vector3.one * 1.2f;
    private Vector3 clickScale = Vector3.one * 0.8f;

    
    [SerializeField] private AudioClip hoverSound;
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private float fadeOutTime = 0.1f;
    private AudioSource audioSource;
    private bool isHovered = false;
    private bool isClicked = false;
    private Coroutine fadeOutCoroutine;

    
    [SerializeField] private MonoBehaviour scriptToActivate1;
    [SerializeField] private MonoBehaviour scriptToActivate2;
    [SerializeField] private MonoBehaviour scriptToActivate3;
    [SerializeField] private MonoBehaviour scriptToActivate4;
    [SerializeField] private MonoBehaviour scriptToActivate5;

    
    private BoxCollider2D boxCollider2D;

    private void Start()
    {
        
        transform.localScale = initialScale;

        
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;

        
        DisableScripts();

        
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

        
        if (boxCollider2D != null)
        {
            boxCollider2D.enabled = false;
        }
    }

    private void OnMouseUp()
    {
        isClicked = false;
        isHovered = true; 
        StartCoroutine(ResetToNormalScale());

        
        EnableScripts();

        
        transform.localScale = initialScale;
    }

    private IEnumerator ResetToNormalScale()
    {
        yield return new WaitForSeconds(0.1f); 
        isHovered = false; 
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
