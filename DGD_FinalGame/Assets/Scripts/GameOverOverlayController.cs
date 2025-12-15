using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverOverlayController : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text highScoreText;

    // void Awake()
    // {
    //     gameObject.SetActive(false);
    // }

    public void Show(int score, int highScore)
    {
        Debug.Log("Overlay Show() called");
        
        gameObject.SetActive(true);

        if (scoreText != null)
            scoreText.text = $"Score: {score}";

        if (highScoreText != null)
            highScoreText.text = $"High Score: {highScore}";
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartScene");
    }
}