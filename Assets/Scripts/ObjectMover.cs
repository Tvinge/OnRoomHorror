using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    [Header("References")]
    public Camera mainCamera;

    [Header("Settings")]
    public float rayDistance = 5f;
    public float moveSpeed = 5f;
    public LayerMask interactableLayer;



    private Transform selectedObject;
    private Rigidbody selectedRigidbody;

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void Update()
    {
        // Handle object selection
        if (Input.GetMouseButtonDown(0))
        {
            SelectObject();
        }

        // Handle object movement
        if (selectedObject != null)
        {
            MoveSelectedObject();
        }

        // Deselect object
        if (Input.GetMouseButtonUp(0))
        {
            DeselectObject();
        }
    }

    void SelectObject()
    {
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Ray ray = mainCamera.ScreenPointToRay(screenCenter);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance, interactableLayer))
        {
            selectedObject = hit.transform;
            selectedRigidbody = selectedObject.GetComponent<Rigidbody>();

            //if (selectedRigidbody != null)
            //{
            //    selectedRigidbody.isKinematic = true; // Disable physics while moving
            //}
        }
    }

    void MoveSelectedObject()
    {
        //Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Ray ray = mainCamera.ScreenPointToRay(screenCenter);

        Plane plane = new Plane(Vector3.up, selectedObject.position);
        float distance;

        if (plane.Raycast(ray, out distance))
        {
            Vector3 targetPosition = ray.GetPoint(distance);
            selectedObject.position = Vector3.Lerp(selectedObject.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    void DeselectObject()
    {
        if (selectedRigidbody != null)
        {
            //selectedRigidbody.isKinematic = false; // Re-enable physics
            selectedRigidbody = null;
        }

        selectedObject = null;
    }
}
