using UnityEngine;

public class SpawnFruits : MonoBehaviour
{
    public GameObject[] fruitPrefabs;
    public Transform spawnPoint;

    private void Start()
    {
        if(spawnPoint == null){
            GetComponentInChildren<Transform>().TryGetComponent(out spawnPoint);
        }
    }

    // This method will be called when a button is clicked
    public void SpawnFruit(int fruitIndex)
    {
        if (fruitIndex >= 0 && fruitIndex < fruitPrefabs.Length)
        {
            Instantiate(fruitPrefabs[fruitIndex], spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            Debug.LogWarning("Invalid fruit index!");
        }
    }
}
