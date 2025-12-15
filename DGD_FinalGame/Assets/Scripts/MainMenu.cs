using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string gameSceneName = "GameScene";
    public string controlsSceneName = "ControlsScene";

    public void PlayGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }
    
    public void ControlsScene()
    {
        SceneManager.LoadScene(controlsSceneName);
    }
}