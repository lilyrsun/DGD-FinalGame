using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private static MusicPlayer instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // prevents double music when loading scenes
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}