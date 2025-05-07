using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    public GameObject[] fruitPrefab;  // Reference to the Fruit Prefab
    public float spawnInterval = 1f;  // Interval between fruit spawns
    public float spawnHeight = 10f;  // Height from which the fruit will spawn
    public float minX = -4f;  // Minimum X position (camera bounds)
    public float maxX = 4f;  // Maximum X position (camera bounds)

    void Start()
    {
        // Start spawning fruits at regular intervals
        InvokeRepeating("SpawnFruit", 0f, spawnInterval);
    }

    void SpawnFruit()
    {
        // Randomize spawn position along the X axis within defined boundaries
        float randomX = Random.Range(minX, maxX);  // Randomize position between minX and maxX
        float spawnZ = 0.5f;  // Set the Z position to 1 (you can change this if needed)
        Vector3 spawnPosition = new Vector3(randomX, spawnHeight, spawnZ);  // Specify spawn position

        // Instantiate the fruit prefab at the calculated position
        int randomIndex = Random.Range(0, fruitPrefab.Length);
        GameObject fruitToSpawn = fruitPrefab[randomIndex];
        Instantiate(fruitToSpawn, spawnPosition, Quaternion.identity, this.transform);
    }
}