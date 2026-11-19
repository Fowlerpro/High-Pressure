using UnityEngine;

public class NoiseTrap : MonoBehaviour
{
    public AIManager enemy;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (enemy != null)
            {
                enemy.HearNoise(transform.position);
            }
        }
    }
}