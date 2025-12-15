using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnHome : MonoBehaviour
{
    public string gameSceneName = "StartScene";

    public void Return()
    {
        SceneManager.LoadScene(gameSceneName);
    }
}