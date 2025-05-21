using UnityEngine;

public class StampBarWorld : MonoBehaviour
{
    [Header("References")]
    public Transform arrow;
    public Transform barTop;
    public Transform barBottom;

    [Header("Results (world-space PNGs)")]
    public Transform underResult;
    public Transform perfectResult;
    public Transform overResult;

    [Header("Movement")]
    public float speed = 2f;
    private bool isMoving = false;

    [Header("Zone Y thresholds")]
    public float underMinY = 0f;
    public float underMaxY = 1.99f;

    public float perfectMinY = 2f;
    public float perfectMaxY = 3f;

    public float overMinY = 3.01f;
    public float overMaxY = 5f;

    private Vector3 underStartPos;
    private Vector3 perfectStartPos;
    private Vector3 overStartPos;

    private Vector3 arrowStartPos = new Vector3(4.7f, 0.7f, 8f);
    private float maxY = 10f;

    private Vector3 resultTargetPos = new Vector3(2f, 1.3f, 7.7f);

    private void Start()
    {
        ResetArrow();
        underStartPos = underResult.position;
        perfectStartPos = perfectResult.position;
        overStartPos = overResult.position;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ResetArrow();
            ResetResultPositions();
            isMoving = true;
        }

        if (isMoving)
        {
            float newY = arrow.position.y + speed * Time.deltaTime;

            if (newY >= maxY)
            {
                newY = maxY;
                arrow.position = new Vector3(arrow.position.x, newY, arrow.position.z);
                isMoving = false;
                ShowOverResult();
                enabled = false;
                return;
            }

            arrow.position = new Vector3(arrow.position.x, newY, arrow.position.z);
        }

        if (Input.GetMouseButtonUp(0) && isMoving)
        {
            isMoving = false;
            CheckArrowZone();
        }
    }

    void CheckArrowZone()
    {
        float y = arrow.position.y;

        if (y >= underMinY && y <= underMaxY)
        {
            underResult.position = resultTargetPos;
        }
        else if (y >= perfectMinY && y <= perfectMaxY)
        {
            perfectResult.position = resultTargetPos;
        }
        else if (y >= overMinY && y <= overMaxY)
        {
            overResult.position = resultTargetPos;
        }

        enabled = false;
    }

    void ShowOverResult()
    {
        overResult.position = resultTargetPos;
    }

    void ResetArrow()
    {
        arrow.position = arrowStartPos;
    }

    void ResetResultPositions()
    {
        underResult.position = underStartPos;
        perfectResult.position = perfectStartPos;
        overResult.position = overStartPos;
    }
}
