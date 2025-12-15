using System.Collections;
using UnityEngine;

public class CatAnimatorController : MonoBehaviour
{
    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void PlayHurt(float unscaledSeconds = 0.2f)
    {
        if (anim == null) return;

        // Make sure animation can advance even when Time.timeScale = 0
        StartCoroutine(HurtRoutine(unscaledSeconds));
    }

    private IEnumerator HurtRoutine(float unscaledSeconds)
    {
        var prevMode = anim.updateMode;
        anim.updateMode = AnimatorUpdateMode.UnscaledTime;

        anim.ResetTrigger("Hurt");
        anim.SetTrigger("Hurt");

        yield return new WaitForSecondsRealtime(unscaledSeconds);

        anim.updateMode = prevMode;
    }
}