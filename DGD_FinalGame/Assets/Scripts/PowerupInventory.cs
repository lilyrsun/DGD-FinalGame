using UnityEngine;

public class PowerupInventory : MonoBehaviour
{
    public static PowerupInventory Instance { get; private set; }

    public int stars = 0;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void AddStar(int amount = 1)
    {
        stars += amount;
        Debug.Log($"Stars collected: {stars}");
    }
}