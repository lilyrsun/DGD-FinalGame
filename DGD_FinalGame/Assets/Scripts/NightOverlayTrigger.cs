using UnityEngine;

public class NightOverlayTrigger : MonoBehaviour
{
    public int triggerScore = 500;
    public GameObject darkOverlay; // drag DarkOverlay here

    private bool triggered;

    void Start()
    {
        if (darkOverlay != null) darkOverlay.SetActive(false);
    }

    void Update()
    {
        if (triggered) return;

        var score = FindAnyObjectByType<ScoreManager>();
        if (score == null) return;

        if (score.Score >= triggerScore)
        {
            triggered = true;
            if (darkOverlay != null) darkOverlay.SetActive(true);
        }
    }
}