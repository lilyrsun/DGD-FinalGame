using TMPro;
using UnityEngine;

public class PowerupUI : MonoBehaviour
{
    public TMP_Text starCountText;

    void Update()
    {
        if (starCountText == null) return;

        int stars = (PowerupInventory.Instance != null) ? PowerupInventory.Instance.stars : 0;
        starCountText.text = $"x{stars}";

    }
}