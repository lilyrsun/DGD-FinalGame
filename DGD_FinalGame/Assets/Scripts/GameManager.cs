using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Lives")]
    public float maxLives = 3f;
    public float lives = 3f;

    [Header("Hit Pause")]
    public float hitPauseSeconds = 0.5f;

    public bool IsGameOver { get; private set; }
    public bool IsHitPaused { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        lives = maxLives;
    }

    public void TakeDamage(float amount)
    {
        if (IsGameOver || IsHitPaused) return;

        lives = Mathf.Max(0f, lives - amount);
        Debug.Log($"Lives: {lives}");

        if (lives <= 0f)
        {
            GameOver();
            return;
        }

        StartCoroutine(HitPauseRoutine());
    }

    private IEnumerator HitPauseRoutine()
    {
        IsHitPaused = true;

        float prevTimeScale = Time.timeScale;
        Time.timeScale = 0f;

        // Must use realtime so the pause still lasts while timescale = 0
        yield return new WaitForSecondsRealtime(hitPauseSeconds);

        Time.timeScale = prevTimeScale;
        IsHitPaused = false;
    }

    private void GameOver()
    {
        IsGameOver = true;
        Time.timeScale = 0f;
        Debug.Log("GAME OVER");
    }
}