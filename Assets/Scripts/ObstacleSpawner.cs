using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Obstacle Prefabs")]
    [Tooltip("Drag and drop your asteroid and comet prefabs here.")]
    [SerializeField] private GameObject[] obstaclePrefabs;

    [Header("Spawn Settings")]
    [Tooltip("Time between obstacle spawns (in seconds).")]
    [SerializeField] private float spawnCooldown = 2f;

    [Header("Vertical Spawn Bounds")]
    [SerializeField] private float minY = -4f;
    [SerializeField] private float maxY = 4f;

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnCooldown)
        {
            SpawnObstacle();
            timer = 0f;
        }
    }

    void SpawnObstacle()
    {
        if (obstaclePrefabs == null || obstaclePrefabs.Length == 0) return;

        int randomIndex = Random.Range(0, obstaclePrefabs.Length);
        GameObject selectedPrefab = obstaclePrefabs[randomIndex];

        float randomY = Random.Range(minY, maxY);
        
        Vector3 spawnPosition = new Vector3(transform.position.x, randomY, 0f);

        Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);
    }
}