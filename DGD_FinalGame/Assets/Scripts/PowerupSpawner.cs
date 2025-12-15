using System.Collections;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    [Header("Prefab")]
    public GameObject starPrefab;

    [Header("Trigger")]
    public int startScore = 300;

    [Header("Spawn Position")]
    public float spawnX = 12f;
    public float minY = 0.5f;
    public float maxY = 3.5f;

    [Header("Timing")]
    public float minTime = 3f;
    public float maxTime = 6f;

    private bool started;

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            // Wait until the score threshold is reached
            if (!started)
            {
                var scoreMgr = FindAnyObjectByType<ScoreManager>();
                if (scoreMgr != null && scoreMgr.Score >= startScore)
                    started = true;

                yield return null;
                continue;
            }

            // Random delay between spawns
            float wait = Random.Range(minTime, maxTime);
            yield return new WaitForSeconds(wait);

            // Don’t spawn during hit pause / game over
            if (GameManager.Instance != null && (GameManager.Instance.IsGameOver || GameManager.Instance.IsHitPaused))
                continue;

            SpawnStar();
        }
    }

    void SpawnStar()
    {
        if (starPrefab == null) return;

        float y = Random.Range(minY, maxY);
        Vector3 pos = new Vector3(spawnX, y, 0f);
        Instantiate(starPrefab, pos, Quaternion.identity);
    }
}