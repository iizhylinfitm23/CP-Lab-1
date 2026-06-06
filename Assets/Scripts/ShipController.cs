using UnityEngine;
using UnityEngine.InputSystem;

public class ShipController : MonoBehaviour
{
    [Header("Movement Settings")]
    [Tooltip("The force used to accelerate the ship up or down.")]
    [SerializeField] private float thrustForce = 50f;
    
    [Tooltip("The maximum vertical velocity of the ship.")]
    [SerializeField] private float maxVelocity = 8f;
    
    [Tooltip("The braking drag applied when movement keys are released (0 means no braking).")]
    [SerializeField] private float brakingDrag = 10f;

    private Rigidbody2D rb;
    private float moveInput;
    private float originalDrag;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        #if UNITY_2022_1_OR_NEWER
        originalDrag = rb.linearDamping;
        #else
        originalDrag = rb.drag;
        #endif
    }

    void Update()
    {
        float up = Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed ? 1f : 0f;
        float down = Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed ? 1f : 0f;
        
        moveInput = up - down;

        if (Mathf.Abs(moveInput) < 0.1f)
        {
            SetDrag(brakingDrag);
        }
        else
        {
            SetDrag(originalDrag);
        }
    }

    void FixedUpdate()
    {
        if (Mathf.Abs(moveInput) > 0.1f)
        {
            rb.AddForce(new Vector2(0f, moveInput * thrustForce), ForceMode2D.Force);
        }

        #if UNITY_2022_1_OR_NEWER
        float clampedY = Mathf.Clamp(rb.linearVelocity.y, -maxVelocity, maxVelocity);
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, clampedY);
        #else
        float clampedY = Mathf.Clamp(rb.velocity.y, -maxVelocity, maxVelocity);
        rb.velocity = new Vector2(rb.velocity.x, clampedY);
        #endif
    }

    private void SetDrag(float value)
    {
        #if UNITY_2022_1_OR_NEWER
        rb.linearDamping = value;
        #else
        rb.drag = value;
        #endif
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
        {
            GameManager.Instance.GameOver();
            Destroy(gameObject); 
        }
    }
}