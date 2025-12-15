using System.Collections;
using UnityEngine;

public class SpeedManager : MonoBehaviour
{
    public static SpeedManager Instance { get; private set; }

    [Header("Speed Ramp")]
    public float startSpeed = 6f;
    public float maxSpeed = 16f;
    public float accelerationPerSecond = 0.08f;

    [Header("Multiplier (Powerups)")]
    [Range(0.1f, 2f)] public float speedMultiplier = 1f;

    private float baseSpeed;
    private Coroutine slowRoutine;

    public float CurrentSpeed => baseSpeed * speedMultiplier;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        baseSpeed = startSpeed;
    }

    void Update()
    {
        baseSpeed = Mathf.Min(maxSpeed, baseSpeed + accelerationPerSecond * Time.deltaTime);
    }

    /// <summary>
    /// Temporarily slows the game by setting speedMultiplier (e.g., 0.6) for durationSeconds.
    /// If called again while active, it refreshes the duration.
    /// </summary>
    public void ApplySlow(float multiplier, float durationSeconds)
    {
        multiplier = Mathf.Clamp(multiplier, 0.05f, 1f);

        if (slowRoutine != null) StopCoroutine(slowRoutine);
        slowRoutine = StartCoroutine(SlowRoutine(multiplier, durationSeconds));
    }

    private IEnumerator SlowRoutine(float multiplier, float durationSeconds)
    {
        float prev = speedMultiplier;
        speedMultiplier = multiplier;

        yield return new WaitForSeconds(durationSeconds);

        speedMultiplier = prev;
        slowRoutine = null;
    }
}