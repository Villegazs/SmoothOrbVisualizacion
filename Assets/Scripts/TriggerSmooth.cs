using UnityEngine;
using System.Collections.Generic;

public class TriggerSmooth : MonoBehaviour
{
    public Animator smoothOrbAnimator;
    public GameObject smoothieObject; 
    public float smoothieEnableDelay = 1.0f;
    public SpawnFruits spawnFruits;

    private int fruitCount = 0;
    private List<GameObject> fruitsInTrigger = new List<GameObject>(); // List to track fruits inside the trigger


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Fruit"))
        {
            fruitCount++;
            fruitsInTrigger.Add(other.gameObject);
            Debug.Log($"Fruit entered. Current count: {fruitCount}");

            // If two fruits are detected, activate animations
            if (fruitCount == 2)
            {
                spawnFruits.alreadyOpen = false;
                Invoke(nameof(EnableSmoothie), smoothieEnableDelay);

                smoothOrbAnimator.SetTrigger("Close");
                smoothOrbAnimator.SetTrigger("Smooth");
                Debug.Log("Activated Close and Smooth animations.");

                Invoke(nameof(DestroyFruits), smoothieEnableDelay);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Fruit"))
        {
            fruitCount--;
            Debug.Log($"Fruit exited. Current count: {fruitCount}");
        }
    }

    private void EnableSmoothie()
    {
        if (smoothieObject != null)
        {
            smoothieObject.SetActive(true);
            Debug.Log("Smoothie object enabled.");
        }
        else
        {
            Debug.LogWarning("Smoothie object is not assigned.");
        }
    }

    private void DestroyFruits()
    {
        foreach (GameObject fruit in fruitsInTrigger)
        {
            if (fruit != null)
            {
                Destroy(fruit);
                Debug.Log($"Destroyed fruit: {fruit.name}");
            }
        }

        fruitCount = 0;
        fruitsInTrigger.Clear();
    }
}