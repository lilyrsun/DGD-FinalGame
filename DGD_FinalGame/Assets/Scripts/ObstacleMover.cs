using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    void Update()
    {
        float speed = SpeedManager.Instance != null ? SpeedManager.Instance.CurrentSpeed : 6f;
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}