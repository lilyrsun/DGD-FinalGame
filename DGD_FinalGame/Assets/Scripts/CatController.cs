using UnityEngine;

public class CatController : MonoBehaviour
{
    [Header("Jump")]
    public float jumpForce = 12f;

    [Header("Ground")]
    public LayerMask groundLayer;
    
    [Header("Jump Tuning")]
    public float fallMultiplier = 5f;     // faster fall
    public float lowJumpMultiplier = 5f;  // shorter hop if you tap jump

    private Rigidbody2D rb;
    private bool isGrounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        
        // Better jump feel: fall faster than rise
        if (rb.linearVelocity.y < 0f)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1f) * Time.deltaTime;
        }
        // If player releases jump early, cut the jump short (optional but recommended)
        else if (rb.linearVelocity.y > 0f && !(Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow)))
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1f) * Time.deltaTime;
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Only count as grounded if we're colliding with something on the ground layer
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            // Optional: ensure the contact is beneath us (prevents "grounded" from side hits)
            foreach (var contact in collision.contacts)
            {
                if (contact.normal.y > 0.5f)
                {
                    isGrounded = true;
                    return;
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            isGrounded = false;
        }
    }
}