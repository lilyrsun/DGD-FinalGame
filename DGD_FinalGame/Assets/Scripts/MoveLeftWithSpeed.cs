using UnityEngine;

public class MoveLeftWithSpeed : MonoBehaviour
{
    public float extraSpeed = 0f;

    void Update()
    {
        float speed = SpeedManager.Instance != null ? SpeedManager.Instance.CurrentSpeed : 6f;
        transform.Translate(Vector3.left * (speed + extraSpeed) * Time.deltaTime);
    }
}