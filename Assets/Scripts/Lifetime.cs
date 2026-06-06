using UnityEngine;

public class Lifetime : MonoBehaviour
{
    [Header("Lifetime Settings")]
    [SerializeField] private float lifetimeDuration = 10f;

    void Start()
    {
        Destroy(gameObject, lifetimeDuration);
    }
}