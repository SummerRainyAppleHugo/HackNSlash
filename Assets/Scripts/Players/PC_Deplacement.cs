using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC_Deplacement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float f_moveSpeed = 5f;
    public float f_acceleration = 10f;
    public float f_deceleration = 15f;

    [Header("Touch Settings")]
    public float f_minJoystickRadius = 50f;
    public float f_maxJoystickRadius = 100f;
    public bool b_showTouchArea = true;

    [Header("Collision Settings")]
    public LayerMask wallLayer;
    public float f_collisionOffset = 0.05f;

    private Rigidbody2D rb;
    private CapsuleCollider2D col;
    private Vector2 smoothVelocity;
    private Vector2 touchStartPos;
    private bool b_isTouching = false;
    private Vector2 currentTouchPos;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();

        // Configuration physique optimisée  
        rb.gravityScale = 0;
        rb.freezeRotation = true;
        rb.collisionDetectionMode = UnityEngine.CollisionDetectionMode2D.Continuous;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    void Update()
    {
        HandleTouchInput();
    }

    void FixedUpdate()
    {
        Vector2 movement = smoothVelocity * Time.fixedDeltaTime;
        Vector2 newPosition = rb.position + movement;

        if (!Physics2D.OverlapCapsule(newPosition, col.size, col.direction, 0, wallLayer))
        {
            rb.MovePosition(newPosition);
        }
        else
        {
            // Test movement on X and Y separately
            Vector2 xMove = new Vector2(movement.x, 0);
            if (!Physics2D.OverlapCapsule(rb.position + xMove, col.size, col.direction, 0, wallLayer))
            {
                rb.MovePosition(rb.position + xMove);
            }

            Vector2 yMove = new Vector2(0, movement.y);
            if (!Physics2D.OverlapCapsule(rb.position + yMove, col.size, col.direction, 0, wallLayer))
            {
                rb.MovePosition(rb.position + yMove);
            }
        }
    }

    void HandleTouchInput()
    {
        Vector2 inputDirection = Vector2.zero;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchStartPos = touch.position;
                    b_isTouching = true;
                    break;

                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    if (b_isTouching)
                    {
                        currentTouchPos = touch.position;
                        Vector2 touchDelta = currentTouchPos - touchStartPos;

                        // Calculate direction based on touch position
                        if (touchDelta.magnitude > f_minJoystickRadius)
                        {
                            inputDirection = touchDelta.normalized;

                            // Apply speed based on distance (with max threshold)
                            float distance = Mathf.Min(touchDelta.magnitude, f_maxJoystickRadius);
                            float speedFactor = Mathf.InverseLerp(f_minJoystickRadius, f_maxJoystickRadius, distance);
                            inputDirection *= speedFactor;
                        }
                    }
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    b_isTouching = false;
                    break;
            }
        }

        // Calculate target velocity
        Vector2 targetVelocity = inputDirection * f_moveSpeed;

        // Smooth movement
        smoothVelocity = Vector2.Lerp(smoothVelocity, targetVelocity,
            (targetVelocity.magnitude > 0.1f ? f_acceleration : f_deceleration) * Time.deltaTime);
    }

    // Optional: Visualize touch area in the editor
    void OnDrawGizmos()
    {
        if (b_showTouchArea && Application.isPlaying && b_isTouching)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(Camera.main.ScreenToWorldPoint(touchStartPos),
                                 Camera.main.orthographicSize * 0.1f);

            Gizmos.color = Color.green;
            Gizmos.DrawLine(Camera.main.ScreenToWorldPoint(touchStartPos),
                          Camera.main.ScreenToWorldPoint(currentTouchPos));
        }
    }
}