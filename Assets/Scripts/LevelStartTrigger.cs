using UnityEngine;

public class LevelStartTrigger : MonoBehaviour
{
    public AIManager enemy;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemy.ActivateEnemy();

            gameObject.SetActive(false);
        }
    }
}