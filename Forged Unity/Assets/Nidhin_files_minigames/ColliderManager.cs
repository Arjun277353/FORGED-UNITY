using UnityEngine;

public class HitboxCollision : MonoBehaviour
{
    public BoxCollider2D checkbox1;
    public GameObject tick1;

    public BoxCollider2D checkbox2;
    public GameObject tick2;

    public BoxCollider2D checkbox3;
    public GameObject button;

    [SerializeField] private float holdDuration = 3f;

    private float holdTime = 0f;
    private bool checkbox1Triggered = false;
    private bool checkbox2Triggered = false;
    private bool checkbox3Triggered = false;

    public bool allCheckboxesChecked => checkbox1Triggered && checkbox2Triggered && checkbox3Triggered;

    [Header("Object To Move On Complete")]
    [SerializeField] private Transform objectToMove;
    [SerializeField] private Vector3 targetPosition = new Vector3(2f, 1.5f, 8f);
    private bool hasMoved = false;

    private void Start()
    {
        tick1.SetActive(false);
        tick2.SetActive(false);
        button.SetActive(false);

        // Optionally disable the object at start
        if (objectToMove != null)
            objectToMove.gameObject.SetActive(false);
    }

    void Update()
    {
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (checkbox1.OverlapPoint(mouseWorldPos))
            {
                Debug.Log("Checkbox 1 clicked\nEnabling TickBox 1");
                tick1.SetActive(true);
                checkbox1Triggered = true;
            }
            else if (checkbox2.OverlapPoint(mouseWorldPos))
            {
                Debug.Log("Checkbox 2 clicked\nEnabling TickBox 2");
                tick2.SetActive(true);
                checkbox2Triggered = true;
            }
        }

        if (Input.GetMouseButton(0))
        {
            if (checkbox3.OverlapPoint(mouseWorldPos))
            {
                holdTime += Time.deltaTime;

                if (!checkbox3Triggered && holdTime >= holdDuration)
                {
                    checkbox3Triggered = true;
                    Debug.Log($"Checkbox 3 held for {holdDuration} seconds!");
                    OnCheckbox3HoldComplete();
                }
            }
            else
            {
                holdTime = 0f;
            }
        }
        else
        {
            holdTime = 0f;
        }

        if (allCheckboxesChecked && !hasMoved)
        {
            Debug.Log("All checkboxes are triggered!");

            // Enable the object before moving it
            objectToMove.gameObject.SetActive(true);

            objectToMove.position = targetPosition;
            hasMoved = true;
        }
    }

    void OnCheckbox3HoldComplete()
    {
        Debug.Log("Function triggered after holding Checkbox 3");
        button.SetActive(true);
    }
}
