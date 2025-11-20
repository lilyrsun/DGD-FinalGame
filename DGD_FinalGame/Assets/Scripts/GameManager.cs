using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Survival Settings")]
    public int startingLives = 3;
    public float deathY = -10f;

    private int lives;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        lives = startingLives;
    }

    void Update()
    {
        // In a more advanced version you'd track bricks in a list,
        // but for a quick prototype we can just scan occasionally.
    }

    public void CheckBrickOutOfBounds(Brick brick)
    {
        if (brick.transform.position.y < deathY)
        {
            LoseLife(brick);
        }
    }

    private void LoseLife(Brick brick)
    {
        lives--;
        Debug.Log($"Brick fell! Lives left: {lives}");
        Destroy(brick.gameObject);

        if (lives <= 0)
        {
            Debug.Log("GAME OVER");
            // later: show UI, stop spawning, restart button, etc.
        }
    }
}