using UnityEngine;
using System.Collections;

public class HoverMoveManager : MonoBehaviour
{
    public Transform[] hoverObjects;    // Assign A, B, C, D here
    public Transform[] relatedSprites;  // Assign 1, 2, 3, 4 here
    public Vector3 targetPosition = new Vector3(0, 5, 8);
    public Vector3 moveLeftOffset = new Vector3(-50, 0, 0);
    public float hoverScale = 1.2f;
    public float pressedScale = 0.8f;
    public float scaleSpeed = 10f;
    public float fadeDuration = 0.5f;
    public float moveDuration = 0.5f;
    public float relatedFadeDuration = 1.0f; // 1 second for related sprite fade-in

    private Vector3[] originalPositions;
    private bool isMoving = false;

    void Start()
    {
        // Store original positions of hover objects
        originalPositions = new Vector3[hoverObjects.Length];
        for (int i = 0; i < hoverObjects.Length; i++)
        {
            originalPositions[i] = hoverObjects[i].position;
            SetOpacity(hoverObjects[i], 1);  // Start fully visible
            SetOpacity(relatedSprites[i], 0);  // Start fully invisible
        }

        Debug.Log("HoverMoveManager initialized.");
    }

    void Update()
    {
        if (isMoving) return;
        DetectHover();
    }

    void DetectHover()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        for (int i = 0; i < hoverObjects.Length; i++)
        {
            if (hit.collider != null && hit.collider.transform == hoverObjects[i])
            {
                hoverObjects[i].localScale = Vector3.Lerp(hoverObjects[i].localScale, Vector3.one * hoverScale, Time.deltaTime * scaleSpeed);

                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log($"Clicked on: {hoverObjects[i].name}");
                    OnObjectClicked(i);
                }
            }
            else
            {
                hoverObjects[i].localScale = Vector3.Lerp(hoverObjects[i].localScale, Vector3.one, Time.deltaTime * scaleSpeed);
            }
        }
    }

    public void OnObjectClicked(int index)
    {
        if (isMoving)
        {
            Debug.Log("Movement already in progress. Click ignored.");
            return;
        }

        StartCoroutine(HandleClick(index));
    }

    private IEnumerator HandleClick(int index)
    {
        isMoving = true;

        // Step 1: Fade out hover objects (A, B, C, D)
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / fadeDuration);

            for (int i = 0; i < hoverObjects.Length; i++)
            {
                SetOpacity(hoverObjects[i], 1 - t);
            }

            yield return null;
        }

        // Step 2: Move hover objects to -50 in X
        elapsed = 0f;
        Vector3[] startPositions = new Vector3[hoverObjects.Length];
        for (int i = 0; i < hoverObjects.Length; i++)
        {
            startPositions[i] = hoverObjects[i].position;
        }

        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / moveDuration);

            for (int i = 0; i < hoverObjects.Length; i++)
            {
                hoverObjects[i].position = Vector3.Lerp(startPositions[i], startPositions[i] + moveLeftOffset, t);
            }

            yield return null;
        }

        // Step 3: Smoothly fade in the related sprite over 1 second
        elapsed = 0f;
        while (elapsed < relatedFadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / relatedFadeDuration);
            SetOpacity(relatedSprites[index], t);
            yield return null;
        }

        // Ensure only the related sprite is visible
        for (int i = 0; i < relatedSprites.Length; i++)
        {
            if (i == index)
            {
                SetOpacity(relatedSprites[i], 1);
                relatedSprites[i].position = targetPosition;
            }
            else
            {
                SetOpacity(relatedSprites[i], 0);
            }
        }

        Debug.Log("Movement and fade complete.");
        isMoving = false;
    }

    // Method to set opacity of any object
    private void SetOpacity(Transform obj, float opacity)
    {
        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            Color color = sr.color;
            color.a = opacity;
            sr.color = color;
        }
    }
}
