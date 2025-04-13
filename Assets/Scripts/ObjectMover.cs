using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    [Header("References")]
    public Camera mainCamera;

    [Header("Settings")]
    public float rayDistance = 5f;
    public float moveSpeed = 5f;
    public LayerMask interactableLayer;

    Transform selectedObject;
    Rigidbody selectedRigidbody;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            SelectObject();
        if (selectedObject != null)
            MoveSelectedObject();
        if (Input.GetMouseButtonUp(0))
            DeselectObject();
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
        }
    }

    void MoveSelectedObject()
    {
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
            selectedRigidbody = null;
        }
        selectedObject = null;
    }
}
