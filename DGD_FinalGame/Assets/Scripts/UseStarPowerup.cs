using UnityEngine;

public class UseStarPowerup : MonoBehaviour
{
    [Header("Input")]
    public KeyCode useKey = KeyCode.E;

    [Header("Effect")]
    [Range(0.1f, 1f)] public float slowMultiplier = 0.6f; // 60% speed
    public float slowDurationSeconds = 10f;

    void Update()
    {
        if (Input.GetKeyDown(useKey))
        {
            TryUseSlowStar();
        }
    }

    void TryUseSlowStar()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver) return;

        if (PowerupInventory.Instance == null || SpeedManager.Instance == null) return;

        // Spend 1 star
        if (PowerupInventory.Instance.stars <= 0) return;
        PowerupInventory.Instance.stars--;

        // Apply slow
        SpeedManager.Instance.ApplySlow(slowMultiplier, slowDurationSeconds);

        Debug.Log($"Used star: Slow active for {slowDurationSeconds}s. Stars left: {PowerupInventory.Instance.stars}");
    }
}