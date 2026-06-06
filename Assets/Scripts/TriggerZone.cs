using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    [Header("Trigger Settings")]
    [SerializeField] private string targetTag = "Obstacle";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag))
        {
            GameManager.Instance.AddScore(1);
        }
    }
}