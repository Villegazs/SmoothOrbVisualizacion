using UnityEngine;
using System.Collections;

public class DetectMouse : MonoBehaviour
{
    private Outline[] childOutlines;
    private Camera mainCamera;
    
    [Header("Orbe Information")]
    [SerializeField] private OrbeInfo orbeInfo; // Asigna el ScriptableObject aqu�
    [SerializeField] private GameObject blackener;

    [Header("Examination Object")]
    [SerializeField] private GameObject examineObjectCopy; // Copy of the object that will be shown when zoomed
    
    [Header("Examination Settings")]
    public float examineDistance = 1.5f; // Distance from camera when examining
    public Vector3 examineOffset = new Vector3(0, 0, 0); // Offset from camera forward
    public Transform originPoints; // Offset from original position
    public float examineTransitionSpeed = 5.0f; // Speed to move to examine position
    
    
    private bool isExamining = false;
    private Coroutine currentTransition;
    
    // Store the original position and rotation of the copy
    private Vector3 copyOriginalPosition;
    private Quaternion copyOriginalRotation;

    public OrbeInfo OrbeInfo { get { return orbeInfo; } }
    
    void Start()
    {
        // Find all Outline components in children
        childOutlines = GetComponentsInChildren<Outline>(true);
        mainCamera = Camera.main;
        
        // Initially disable all outlines and the examine object
        SetOutlinesEnabled(false);
        
        if (examineObjectCopy != null)
        {
            // Save the original position and rotation of the copy
            copyOriginalPosition = originPoints.position;
            copyOriginalRotation = examineObjectCopy.transform.rotation;
            
            examineObjectCopy.SetActive(false);
            Debug.Log($"Saved examination copy's original position: {copyOriginalPosition}");
        }
        else
        {
            Debug.LogError("Examine object copy is not assigned!");
        }
        
        Debug.Log($"Found {childOutlines.Length} Outline components in children");
    }

    void Update()
    {
        // If we're examining and Escape is pressed, end examination
        if (isExamining && Input.GetKeyDown(KeyCode.Escape))
        {
            EndExamination();
            return;
        }
        
        // Don't cast rays if we're examining
        if (isExamining) return;
        
        // Cast a ray from the mouse position
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        // Check if the ray hits this object's collider
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                // Mouse is hovering over this object
                SetOutlinesEnabled(true);
                
                // Check for click to examine
                if (Input.GetMouseButtonDown(0)) // Left mouse button
                {
                    StartExamination();
                }
            }
            else
            {
                // Mouse is hovering over something else
                SetOutlinesEnabled(false);
            }
        }
        else
        {
            // Ray didn't hit anything
            SetOutlinesEnabled(false);
        }
    }
    
    private void SetOutlinesEnabled(bool enabled)
    {
        foreach (Outline outline in childOutlines)
        {
            if (outline != null && outline.enabled != enabled)
            {
                outline.enabled = enabled;
            }
        }
    }
    
    private void StartExamination()
    {
        if (examineObjectCopy == null) return;
        
        isExamining = true;
        
        // Start with the copy at the original object's position for transition
        examineObjectCopy.transform.position = transform.position;
        examineObjectCopy.transform.rotation = transform.rotation;
        examineObjectCopy.SetActive(true);
        
        // Calculate the target position in front of the camera
        Vector3 targetPosition = mainCamera.transform.position + 
                                mainCamera.transform.forward * examineDistance + 
                                examineOffset;
        
        // Start smooth transition to examination position
        if (currentTransition != null)
            StopCoroutine(currentTransition);
            
        currentTransition = StartCoroutine(SmoothTransition(examineObjectCopy.transform, 
                                          targetPosition, 
                                          examineObjectCopy.transform.rotation, 
                                          examineTransitionSpeed));
        
        Debug.Log("Started examination mode with smooth transition");
    }
    
    public void EndExamination()
    {
        if (examineObjectCopy == null) return;
        
        // Start smooth transition back to the copy's original position
        if (currentTransition != null)
            StopCoroutine(currentTransition);
            
        currentTransition = StartCoroutine(SmoothTransitionOut(examineObjectCopy.transform, 
                                          originPoints.position, // Use saved original position
                                          copyOriginalRotation, // Use saved original rotation
                                          examineTransitionSpeed));
        
        blackener.SetActive(false);
        Debug.Log("Ending examination mode with smooth transition");
    }
    
    private IEnumerator SmoothTransition(Transform objectTransform, Vector3 targetPosition, Quaternion targetRotation, float speed)
    {
        Vector3 startPosition = originPoints.position;
        Quaternion startRotation = objectTransform.rotation;
        float journeyLength = Vector3.Distance(startPosition, targetPosition);
        float startTime = Time.time;
        
        // Transition until we reach the target position
        while (Vector3.Distance(objectTransform.position, targetPosition) > 0.01f)
        {
            float distanceCovered = (Time.time - startTime) * speed;
            float fractionOfJourney = distanceCovered / journeyLength;
            
            objectTransform.position = Vector3.Lerp(startPosition, targetPosition, fractionOfJourney);
            objectTransform.rotation = Quaternion.Slerp(startRotation, targetRotation, fractionOfJourney);
            
            yield return null;
        }
        
        // Ensure exact final position
        objectTransform.position = targetPosition;
        objectTransform.rotation = targetRotation;
    }
    
    private IEnumerator SmoothTransitionOut(Transform objectTransform, Vector3 targetPosition, Quaternion targetRotation, float speed)
    {
        Vector3 startPosition = objectTransform.position;
        Quaternion startRotation = objectTransform.rotation;
        float journeyLength = Vector3.Distance(startPosition, targetPosition);
        float startTime = Time.time;
        
        // Transition until we reach the target position
        while (Vector3.Distance(objectTransform.position, targetPosition) > 0.01f)
        {
            float distanceCovered = (Time.time - startTime) * speed;
            float fractionOfJourney = distanceCovered / journeyLength;
            
            objectTransform.position = Vector3.Lerp(startPosition, targetPosition, fractionOfJourney);
            objectTransform.rotation = Quaternion.Slerp(startRotation, targetRotation, fractionOfJourney);
            
            yield return null;
        }
        
        // Ensure exact final position
        objectTransform.position = targetPosition;
        objectTransform.rotation = targetRotation;
        
        // Disable the examination copy and reset examining flag
        examineObjectCopy.SetActive(false);
        isExamining = false;
    }
    
    // Alternative Unity event methods
    private void OnMouseEnter()
    {
        if (!isExamining)
        {
            SetOutlinesEnabled(true);
        }
    }
    
    private void OnMouseExit()
    {
        if (!isExamining)
        {
            SetOutlinesEnabled(false);
        }
    }
    
    private void OnMouseDown()
    {
        if (!isExamining)
        {
            StartExamination();
            SwitchPanels("PartsOfOrbMenu|InfoPartOrbMenu");
            
        }
    }
    private void SwitchPanels(string panelName)
    {
            blackener.SetActive(true);
            UIManager.Instance.SwitchPanels(panelName);
            UIManager.Instance.ShowOrbeInfo(orbeInfo,this);
    }
}