using UnityEngine;

public class SpeedManager : MonoBehaviour
{
    public static SpeedManager Instance { get; private set; }

    [Header("Speed")]
    public float startSpeed = 6f;
    public float maxSpeed = 16f;
    public float accelerationPerSecond = 0.08f;

    public float CurrentSpeed { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        CurrentSpeed = startSpeed;
    }

    void Update()
    {
        // Smoothly ramp speed up over time
        CurrentSpeed = Mathf.Min(maxSpeed, CurrentSpeed + accelerationPerSecond * Time.deltaTime);
    }

    public void AddTemporarySpeed(float amount)
    {
        CurrentSpeed = Mathf.Min(maxSpeed, CurrentSpeed + amount);
    }

    public void ResetSpeed()
    {
        CurrentSpeed = startSpeed;
    }
}