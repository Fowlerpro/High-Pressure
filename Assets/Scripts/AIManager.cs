using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class AIManager : MonoBehaviour
{
    public Transform player;

    public float normalSpeed = 3f;
    public float aggressiveSpeed = 6f;

    NavMeshAgent agent;

    bool active = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = normalSpeed;
        agent.isStopped = true;
    }

    void Update()
    {
        if (active)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
        }
    }

    public void ActivateEnemy()
    {
        active = true;
        agent.speed = normalSpeed;
    }

    public void BecomeAggressive()
    {
        active = true;
        agent.speed = aggressiveSpeed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(
                SceneManager.GetActiveScene().name
            );
        }
    }
}