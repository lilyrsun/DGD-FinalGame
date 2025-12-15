using UnityEngine;

public class ClickableStar : MonoBehaviour
{
    void OnMouseDown()
    {
        // Don’t allow clicking during hit pause / game over
        if (GameManager.Instance != null && (GameManager.Instance.IsGameOver || GameManager.Instance.IsHitPaused))
            return;

        if (PowerupInventory.Instance != null)
            PowerupInventory.Instance.AddStar(1);

        Destroy(gameObject);
    }
}