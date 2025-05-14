using UnityEngine;

public class RotateWithMouse : MonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField] private float rotationSpeed = 5.0f;
    [SerializeField] private bool invertXRotation = false;
    [SerializeField] private bool invertYRotation = false;
    
    // Cached reference to camera
    private Camera mainCamera;
    
    void Start()
    {
        mainCamera = Camera.main;
    }
    
    void Update()
    {
        HandleRotation();
    }
    
    private void HandleRotation()
    {
        // Rotate object based on mouse movement when holding left mouse button
        if (Input.GetMouseButton(0))
        {
            float rotX = Input.GetAxis("Mouse X") * rotationSpeed;
            float rotY = Input.GetAxis("Mouse Y") * rotationSpeed;
            
            // Apply inversion if needed
            if (invertXRotation) rotX = -rotX;
            if (invertYRotation) rotY = -rotY;
            
            // Rotate around global up axis (Y) based on horizontal mouse movement
            transform.Rotate(Vector3.up, -rotX, Space.World);
            
            // Rotate around local right axis (X) based on vertical mouse movement
            transform.Rotate(Vector3.right, rotY, Space.Self);
        }
    }
}