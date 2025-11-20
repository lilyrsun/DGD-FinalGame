using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Brick : MonoBehaviour
{
    public float sleepVelocityThreshold = 0.05f;
    public float checkDelay = 0.5f;

    private Rigidbody2D rb;
    private BrickSpawner spawner;
    private bool hasLanded = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        // Find the spawner in the scene once
        spawner = FindObjectOfType<BrickSpawner>();
        // Slight delay before we start checking for "sleep" to avoid instant landing
        Invoke(nameof(StartLandingCheck), checkDelay);
    }
    
    void Update()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.CheckBrickOutOfBounds(this);
        }
    }

    private void StartLandingCheck()
    {
        StartCoroutine(CheckIfLanded());
    }

    private System.Collections.IEnumerator CheckIfLanded()
    {
        while (!hasLanded)
        {
            // If the brick is very slow and roughly not moving, consider it landed
            if (rb.linearVelocity.magnitude < sleepVelocityThreshold)
            {
                hasLanded = true;
                rb.bodyType = RigidbodyType2D.Dynamic; // stays dynamic, but we could set constraints if needed

                if (spawner != null)
                {
                    spawner.OnBrickLanded();
                }

                yield break;
            }

            yield return new WaitForSeconds(0.1f);
        }
    }
}