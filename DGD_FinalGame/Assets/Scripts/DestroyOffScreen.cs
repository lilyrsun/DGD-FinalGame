using UnityEngine;

public class DestroyOffscreen : MonoBehaviour
{
    public float destroyX = -15f; // adjust based on your camera framing

    void Update()
    {
        if (transform.position.x < destroyX)
            Destroy(gameObject);
    }
}