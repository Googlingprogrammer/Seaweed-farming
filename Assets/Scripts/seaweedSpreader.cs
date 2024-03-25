using UnityEngine;

public class seaweedSpawner : MonoBehaviour
{
    public GameObject prefab; // Prefab to instantiate
    public int numberOfInstances = 10; // Number of instances to create
    public Vector2 xRange = new Vector2(10f, 190f); // Range of x values
    public float yValue = 0f; // Fixed y value
    public Vector2 zRange = new Vector2(10f, 190f); // Range of z values

    private bool hasSpawned = false;

    void Start()
    {
        if (!hasSpawned)
        {
            SpawnPrefabs();
            hasSpawned = true;
            // Disable the script after spawning
            enabled = false;
        }
    }

    void SpawnPrefabs()
    {
        // Loop to instantiate prefabs
        for (int i = 0; i < numberOfInstances; i++)
        {
            // Calculate random positions within specified ranges
            float xPos = Random.Range(xRange.x, xRange.y);
            float zPos = Random.Range(zRange.x, zRange.y);

            // Create new instance at the calculated position
            Vector3 spawnPosition = new Vector3(xPos, yValue, zPos);
            Instantiate(prefab, spawnPosition, Quaternion.identity);
        }
    }
}
