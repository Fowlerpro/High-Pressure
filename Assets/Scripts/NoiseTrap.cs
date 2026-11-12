using UnityEngine;

public class NoiseTrap : MonoBehaviour
{
    public AIManager enemy;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemy.BecomeAggressive();
            enemy.Ping(transform.position);
        }
    }
}