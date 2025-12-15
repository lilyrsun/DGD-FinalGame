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
    
    [Header("Invulnerability")]
    public float invulnSeconds = 0.8f;
    public bool IsInvulnerable { get; private set; }
    
    public GameOverOverlayController gameOverOverlay;


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
        if (IsGameOver || IsHitPaused || IsInvulnerable) return;

        lives = Mathf.Max(0f, lives - amount);
        Debug.Log($"Lives: {lives}");
		
		FindAnyObjectByType<CatSFX>()?.PlayHurt();

		var catAnim = FindAnyObjectByType<CatAnimatorController>(); 
		if (catAnim != null) 
			catAnim.PlayHurt(hitPauseSeconds); // show hurt during the pause

        if (lives <= 0f)
        {
            GameOver();
            return;
        }

        StartCoroutine(HitPauseRoutine());
        StartCoroutine(InvulnRoutine());
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
    
    private IEnumerator InvulnRoutine()
    {
        IsInvulnerable = true;
        yield return new WaitForSecondsRealtime(invulnSeconds); // ignore timescale
        IsInvulnerable = false;
    }

    private void GameOver()
    {
        IsGameOver = true;
        Time.timeScale = 0f;

        int score = 0;
        int high = 0;

        var scoreMgr = FindAnyObjectByType<ScoreManager>();
        if (scoreMgr != null)
        {
            score = scoreMgr.Score;
            high = scoreMgr.HighScore; // use your ScoreManager’s stored high score
        }
        
        Debug.Log("GameOver() called");


        if (gameOverOverlay != null)
            gameOverOverlay.Show(score, high);
        else
            Debug.LogError("GameOverOverlay reference is missing on GameManager!");
    }


}