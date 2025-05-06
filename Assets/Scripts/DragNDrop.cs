using UnityEngine;

public class DragNDrop : MonoBehaviour
{
    private Camera mainCamera;
    private Rigidbody selectedRigidbody;
    private Vector3 offset; 
    [SerializeField] private float zDepth;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Detect left mouse button press
        {
            Debug.Log("Mouse button down detected.");
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            int layerMask = ~LayerMask.GetMask("Ignore Raycast");

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
            {
                Debug.Log($"Raycast hit: {hit.collider.name}");
                if (hit.collider != null && hit.collider.attachedRigidbody != null)
                {
                    selectedRigidbody = hit.collider.attachedRigidbody;
                    offset = selectedRigidbody.position - GetMouseWorldPosition();
                    selectedRigidbody.useGravity = false;
                    Debug.Log($"Object selected: {selectedRigidbody.name}");
                }
                else
                {
                    Debug.Log("No Rigidbody attached to the hit object.");
                }
            }
            else
            {
                Debug.Log("Raycast did not hit any object.");
            }
        }

        if (Input.GetMouseButton(0) && selectedRigidbody != null) // Drag the object
        {
            Vector3 targetPosition = GetMouseWorldPosition() + offset;
            selectedRigidbody.MovePosition(targetPosition);
            Debug.Log($"Dragging object: {selectedRigidbody.name} to position {targetPosition}");
        }

        if (Input.GetMouseButtonUp(0)) // Release the object
        {
            if (selectedRigidbody != null)
            {
                selectedRigidbody.useGravity = true;
                Debug.Log($"Released object: {selectedRigidbody.name}");
            }
            selectedRigidbody = null;
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = zDepth; // Maintain the object's depth
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
        Debug.Log($"Mouse world position: {worldPosition}");
        return worldPosition;
    }
}