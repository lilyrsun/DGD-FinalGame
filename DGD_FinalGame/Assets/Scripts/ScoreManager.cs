using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text scoreText;
    public TMP_Text hiText; // optional

    [Header("Scoring")]
    public float pointsPerSecond = 10f;

    private float scoreFloat = 0f;

    public int Score { get; private set; }
    public int HighScore { get; private set; }

    void Start()
    {
        HighScore = PlayerPrefs.GetInt("HIGH_SCORE", 0);
        UpdateUI();
    }

    void Update()
    {
        // Pause score during hit-pause or game over
        if (GameManager.Instance != null && (GameManager.Instance.IsGameOver || GameManager.Instance.IsHitPaused))
            return;

        scoreFloat += pointsPerSecond * Time.deltaTime;
        Score = Mathf.FloorToInt(scoreFloat);

        if (Score > HighScore)
        {
            HighScore = Score;
            PlayerPrefs.SetInt("HIGH_SCORE", HighScore);
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = Score.ToString("00000"); // Dino-style

        if (hiText != null)
            hiText.text = $"HI {HighScore:00000}";
    }
}