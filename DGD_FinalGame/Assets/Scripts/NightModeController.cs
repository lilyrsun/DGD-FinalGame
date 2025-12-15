using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class NightModeController : MonoBehaviour
{
    [Header("Trigger")]
    public int triggerScore = 500;
    public bool triggerOnce = true;

    [Header("Duration")]
    public float durationSeconds = 10f; // set to 9999 if you want it to stay dark

    [Header("Lights (URP 2D)")]
    public Light2D globalLight;
    public Light2D catPointLight;

    [Header("Night Settings")] public float nightGlobalIntensity = 0f;
    public float nightCatIntensity = 1.2f;
    public float nightOuterRadius = 4.0f;

    [Header("Transition")]
    public float fadeSeconds = 0.6f;

    private bool active = false;
    private bool hasTriggered = false;

    private float dayGlobalIntensity;
    private float dayCatIntensity;
    private float dayOuterRadius;

    void Start()
    {
        // Save original values (daytime)
        dayGlobalIntensity = globalLight.intensity;
        dayCatIntensity = catPointLight.intensity;
        dayOuterRadius = catPointLight.pointLightOuterRadius;
    }

    void Update()
    {
        if (GameManager.Instance != null && (GameManager.Instance.IsGameOver || GameManager.Instance.IsHitPaused))
            return;

        if (hasTriggered && triggerOnce) return;

        // Find score
        var scoreMgr = FindAnyObjectByType<ScoreManager>();
        if (scoreMgr == null) return;

        if (!active && scoreMgr.Score >= triggerScore)
        {
            hasTriggered = true;
            StartCoroutine(NightRoutine());
        }
    }

    IEnumerator NightRoutine()
    {
        active = true;

        // Fade into night
        yield return StartCoroutine(FadeLights(
            fromGlobal: dayGlobalIntensity, toGlobal: nightGlobalIntensity,
            fromCat: dayCatIntensity, toCat: nightCatIntensity,
            fromRadius: dayOuterRadius, toRadius: nightOuterRadius,
            seconds: fadeSeconds
        ));

        // Stay dark
        yield return new WaitForSeconds(durationSeconds);

        // Fade back to day
        yield return StartCoroutine(FadeLights(
            fromGlobal: nightGlobalIntensity, toGlobal: dayGlobalIntensity,
            fromCat: nightCatIntensity, toCat: dayCatIntensity,
            fromRadius: nightOuterRadius, toRadius: dayOuterRadius,
            seconds: fadeSeconds
        ));

        active = false;
    }

    IEnumerator FadeLights(float fromGlobal, float toGlobal, float fromCat, float toCat,
                           float fromRadius, float toRadius, float seconds)
    {
        float t = 0f;

        while (t < seconds)
        {
            t += Time.deltaTime;
            float a = seconds <= 0f ? 1f : Mathf.Clamp01(t / seconds);

            globalLight.intensity = Mathf.Lerp(fromGlobal, toGlobal, a);
            catPointLight.intensity = Mathf.Lerp(fromCat, toCat, a);
            catPointLight.pointLightOuterRadius = Mathf.Lerp(fromRadius, toRadius, a);

            yield return null;
        }

        globalLight.intensity = toGlobal;
        catPointLight.intensity = toCat;
        catPointLight.pointLightOuterRadius = toRadius;
    }
}
