using UnityEngine;

public class ObstacleInfo : MonoBehaviour
{
    // How wide this obstacle is in world units (used for spacing).
    // If you leave this at 0, the spawner will try to auto-detect using Collider2D bounds.
    public float widthOverride = 0f;

    public float GetWidth()
    {
        if (widthOverride > 0f) return widthOverride;

        var col = GetComponent<Collider2D>();
        if (col != null) return col.bounds.size.x;

        // Fallback if no collider (not recommended)
        return 1f;
    }
}