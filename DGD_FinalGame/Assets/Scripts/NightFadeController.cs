using System.Collections;
using UnityEngine;

public class NightFadeController : MonoBehaviour
{
    [Header("Trigger")]
    public int interval = 500;          // every 500 points
    public int startAt = 500;           // first trigger score

    [Header("Timing")]
    public float fadeInSeconds = 0.8f;
    public float nightDurationSeconds = 10f;
    public float fadeOutSeconds = 0.8f;

    [Header("Refs")]
    public SpriteRenderer darkOverlayRenderer; // Drag DarkOverlay SpriteRenderer here
    public Transform moonMask;                 // Drag MoonMask Transform here (optional)

    [Header("Mask Radius (Optional)")]
    public float maskScaleNight = 7f;
    public float maskScaleDay = 7f;

    private int nextTriggerScore;
    private bool nightRunning;

    void Start()
    {
        nextTriggerScore = startAt;

        // Ensure overlay is enabled but transparent at start
        if (darkOverlayRenderer != null)
        {
            darkOverlayRenderer.gameObject.SetActive(true);
            SetOverlayAlpha(0f);
        }

        if (moonMask != null)
            moonMask.localScale = new Vector3(maskScaleDay, maskScaleDay, 1f);
    }

    void Update()
    {
        if (nightRunning) return;

        var scoreMgr = FindAnyObjectByType<ScoreManager>();
        if (scoreMgr == null) return;

        if (scoreMgr.Score >= nextTriggerScore)
        {
            // schedule the next time BEFORE starting routine, so it’s ready for 1000/1500/...
            nextTriggerScore += interval;
            StartCoroutine(NightRoutine());
        }
    }

    IEnumerator NightRoutine()
    {
        nightRunning = true;

        // Fade in
        yield return StartCoroutine(FadeOverlayAlpha(0f, 1f, fadeInSeconds));
        yield return StartCoroutine(FadeMaskScale(maskScaleDay, maskScaleNight, fadeInSeconds));

        // Stay night (uses game time; pauses during hit-pause)
        yield return new WaitForSeconds(nightDurationSeconds);

        // Fade out
        yield return StartCoroutine(FadeOverlayAlpha(1f, 0f, fadeOutSeconds));
        yield return StartCoroutine(FadeMaskScale(maskScaleNight, maskScaleDay, fadeOutSeconds));

        nightRunning = false;
    }

    IEnumerator FadeOverlayAlpha(float from, float to, float seconds)
    {
        if (darkOverlayRenderer == null) yield break;

        float t = 0f;
        while (t < seconds)
        {
            t += Time.deltaTime;
            float a = seconds <= 0f ? 1f : Mathf.Clamp01(t / seconds);
            SetOverlayAlpha(Mathf.Lerp(from, to, a));
            yield return null;
        }
        SetOverlayAlpha(to);
    }

    IEnumerator FadeMaskScale(float from, float to, float seconds)
    {
        if (moonMask == null) yield break;

        float t = 0f;
        Vector3 start = new Vector3(from, from, 1f);
        Vector3 end = new Vector3(to, to, 1f);

        while (t < seconds)
        {
            t += Time.deltaTime;
            float a = seconds <= 0f ? 1f : Mathf.Clamp01(t / seconds);
            moonMask.localScale = Vector3.Lerp(start, end, a);
            yield return null;
        }
        moonMask.localScale = end;
    }

    void SetOverlayAlpha(float alpha)
    {
        if (darkOverlayRenderer == null) return;
        Color c = darkOverlayRenderer.color;
        c.a = alpha;
        darkOverlayRenderer.color = c;
    }
}
