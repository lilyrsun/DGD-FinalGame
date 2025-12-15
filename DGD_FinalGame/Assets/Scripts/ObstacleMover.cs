using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver) return;

        float speed = SpeedManager.Instance != null ? SpeedManager.Instance.CurrentSpeed : 6f;
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}