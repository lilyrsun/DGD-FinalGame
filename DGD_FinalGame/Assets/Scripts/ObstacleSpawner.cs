using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject[] obstaclePrefabs;

    [Header("Spawn Position")]
    public float spawnX = 12f;
    public float groundY = -2.8f;

    [Header("Timing (still used, but won't violate spacing)")]
    public float minSpawnTime = 0.9f;
    public float maxSpawnTime = 1.6f;

    [Header("Spacing Safety")]
    public float minGap = 2.5f;          // minimum empty space between obstacles
    public float extraGapAtSpeed = 0.0f; // optional: increase gap later when speed ramps

    [Header("Difficulty Ramp")]
    public float timeToHarder = 90f;     // ramps timing down over time

    private Transform lastSpawned;
    private float lastSpawnedWidth = 1f;

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        float elapsed = 0f;

        while (true)
        {
            // Time-based randomness (like Dino), but spacing still must be satisfied
            float t = Mathf.Clamp01(elapsed / timeToHarder);
            float curMin = Mathf.Lerp(minSpawnTime, minSpawnTime * 0.6f, t);
            float curMax = Mathf.Lerp(maxSpawnTime, maxSpawnTime * 0.6f, t);

            float wait = Random.Range(curMin, curMax);
            yield return new WaitForSeconds(wait);
            elapsed += wait;

            // NEW: spacing gate (wait until it's safe to spawn)
            yield return new WaitUntil(CanSpawnNext);

            SpawnOne();
        }
    }

    bool CanSpawnNext()
    {
        if (lastSpawned == null) return true;

        // last obstacle must have moved left far enough from the spawn point
        // Condition: last obstacle's right edge is left of (spawnX - minGap)
        float lastRightEdge = lastSpawned.position.x + (lastSpawnedWidth * 0.5f);
        float required = spawnX - (minGap + extraGapAtSpeed);

        return lastRightEdge < required;
    }

    void SpawnOne()
    {
        if (obstaclePrefabs == null || obstaclePrefabs.Length == 0) return;

        var prefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
        Vector3 pos = new Vector3(spawnX, groundY, 0f);

        GameObject obj = Instantiate(prefab, pos, Quaternion.identity);

        // Track last spawned obstacle + its width
        lastSpawned = obj.transform;

        var info = obj.GetComponent<ObstacleInfo>();
        if (info != null) lastSpawnedWidth = info.GetWidth();
        else
        {
            var col = obj.GetComponent<Collider2D>();
            lastSpawnedWidth = (col != null) ? col.bounds.size.x : 1f;
        }
    }
}
