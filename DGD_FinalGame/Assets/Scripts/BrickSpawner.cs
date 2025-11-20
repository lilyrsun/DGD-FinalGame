using UnityEngine;
using UnityEngine.InputSystem; // new Input System

public class BrickSpawner : MonoBehaviour
{
    [Header("Brick Settings")]
    public GameObject brickPrefab;
    public float moveSpeed = 10f;
    public float fallSpeedMultiplier = 2f;

    [Header("Spawn Area")]
    public float minX = -7f;
    public float maxX = 7f;

    private GameObject currentBrick;
    private Rigidbody2D currentRb;

    private bool hasBrick => currentBrick != null;

    void Start()
    {
        SpawnNewBrick();
    }

    void Update()
    {
        if (!hasBrick) return;

        HandleHorizontalMovement();
        HandleRotation();
        HandleSoftDrop();
    }

    private void HandleHorizontalMovement()
    {
        var keyboard = Keyboard.current;
        float dir = 0f;

        if (keyboard.leftArrowKey.isPressed) dir -= 1f;
        if (keyboard.rightArrowKey.isPressed) dir += 1f;

        if (dir != 0f)
        {
            // Move spawner horizontally
            Vector3 pos = transform.position;
            pos.x += dir * moveSpeed * Time.deltaTime;
            pos.x = Mathf.Clamp(pos.x, minX, maxX);
            transform.position = pos;

            // Snap brick to spawner's x position while it's still falling freely
            if (currentBrick != null)
            {
                Vector3 brickPos = currentBrick.transform.position;
                brickPos.x = transform.position.x;
                currentBrick.transform.position = brickPos;
            }
        }
    }

    private void HandleRotation()
    {
        var keyboard = Keyboard.current;

        if (keyboard.upArrowKey.wasPressedThisFrame)
        {
            currentBrick.transform.Rotate(0f, 0f, -90f); // clockwise
        }

        if (keyboard.spaceKey.wasPressedThisFrame)
        {
            currentBrick.transform.Rotate(0f, 0f, 90f); // counter-clockwise
        }
    }

    private void HandleSoftDrop()
    {
        var keyboard = Keyboard.current;

        if (keyboard.downArrowKey.isPressed && currentRb != null)
        {
            // Add a little downward velocity
            var v = currentRb.linearVelocity;
            v.y = -Mathf.Abs(Physics2D.gravity.y) * fallSpeedMultiplier;
            currentRb.linearVelocity = v;
        }
    }

    private void SpawnNewBrick()
    {
        Vector3 spawnPos = transform.position;
        currentBrick = Instantiate(brickPrefab, spawnPos, Quaternion.identity);
        currentRb = currentBrick.GetComponent<Rigidbody2D>();

        // Optional: small random rotation / size tweaks later
    }

    // Called by the brick when it has "settled"
    public void OnBrickLanded()
    {
        currentBrick = null;
        currentRb = null;
        SpawnNewBrick();
    }
}
