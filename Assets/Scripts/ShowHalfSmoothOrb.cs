using UnityEngine;
using System.Collections;
using Unity.Cinemachine;

public class ShowHalfSmoothOrb : MonoBehaviour
{
    [Header("Game Objects")]
    public GameObject completeSphere;
    public GameObject halvedSphere;

    [Header("Cameras")]
    public CinemachineCamera mainCamera;
    public CinemachineCamera secondaryCamera;

    [Header("Transition Settings")]
    public float fadeTime = 1.0f;

    private bool isShowingHalvedSphere = false;

        // Function called by the switch button
    public void SwitchToHalvedSphere()
    {
        if (!isShowingHalvedSphere)
            StartCoroutine(TransitionToHalvedSphere());
    }

    // Function called by the reset button
    public void ResetToCompleteSphere()
    {
        if (isShowingHalvedSphere)
            StartCoroutine(TransitionToCompleteSphere());
    }

    // Coroutine to handle the transition to halved sphere
    private IEnumerator TransitionToHalvedSphere()
    {
        // Ensure we can't trigger multiple transitions
        isShowingHalvedSphere = true;

        // Fade out complete sphere using DOTween
        yield return null;
        
        // Switch models and cameras
        completeSphere.SetActive(false);
        halvedSphere.SetActive(true);
        
        mainCamera.gameObject.SetActive(false);
        secondaryCamera.gameObject.SetActive(true);
        
        yield return null;
    }

    // Coroutine to handle the transition back to complete sphere
    private IEnumerator TransitionToCompleteSphere()
    {
        // Ensure we can't trigger multiple transitions
        isShowingHalvedSphere = false;

        // Fade out halved sphere using DOTween
        yield return null;
        
        // Switch models and cameras back
        halvedSphere.SetActive(false);
        completeSphere.SetActive(true);
        
        secondaryCamera.gameObject.SetActive(false);
        mainCamera.gameObject.SetActive(true);

        yield return null;
    }
}
