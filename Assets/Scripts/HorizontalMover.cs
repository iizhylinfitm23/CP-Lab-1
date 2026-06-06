using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HorizontalMover : MonoBehaviour
{
    [Header("Speed Settings")]
    [SerializeField] private float minSpeed = 3f;
    [SerializeField] private float maxSpeed = 5f;

    [Header("Rotation Settings")]
    [SerializeField] private bool canRotate = false;
    [SerializeField] private float rotationMultiplier = 30f;

    [Header("Angle Settings")]
    [SerializeField] private bool launchAtAngle = false;
    [SerializeField] private float maxAngle = 30f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        float randomSpeed = Random.Range(minSpeed, maxSpeed);
        Vector2 moveDirection = Vector2.left;

        if (launchAtAngle)
        {
            float randomAngle = Random.Range(-maxAngle, maxAngle);
            moveDirection = Quaternion.Euler(0, 0, randomAngle) * Vector2.left;
        }

        #if UNITY_2022_1_OR_NEWER
        rb.linearVelocity = moveDirection * randomSpeed;
        #else
        rb.velocity = moveDirection * randomSpeed;
        #endif

        if (canRotate)
        {
            float calculatedRotationSpeed = randomSpeed * rotationMultiplier;
            rb.angularVelocity = calculatedRotationSpeed;
        }
    }
}