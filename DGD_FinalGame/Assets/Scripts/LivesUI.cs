using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour
{
    [Header("Assign 3 paw Images left-to-right")]
    public Image[] paws;

    [Header("Colors")]
    public Color aliveColor = Color.white;
    public Color deadColor = new Color(0.5f, 0.5f, 0.5f, 1f); // grey

    void Update()
    {
        if (GameManager.Instance == null) return;

        // If your lives are float, convert to int paws (3,2,1,0)
        int livesInt = Mathf.CeilToInt(GameManager.Instance.lives);

        for (int i = 0; i < paws.Length; i++)
        {
            if (paws[i] == null) continue;

            // i=0 is first paw. If livesInt=2, paw0 and paw1 are alive, paw2 is grey.
            paws[i].color = (i < livesInt) ? aliveColor : deadColor;
        }
    }
}