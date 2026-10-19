using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class AIManager : MonoBehaviour
{
    public Transform player;

    public float chaseDistance = 10f;
    public float attackDistance = 2f;
    public float moveSpeed = 3f;

    NavMeshAgent agent;

    enum EnemyState
    {
        Idle,
        Chase,
        Attack
    }

    EnemyState currentState;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.speed = moveSpeed;
        agent.stoppingDistance = attackDistance;

        currentState = EnemyState.Idle;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackDistance)
        {
            currentState = EnemyState.Attack;
        }
        else if (distance <= chaseDistance)
        {
            currentState = EnemyState.Chase;
        }
        else
        {
            currentState = EnemyState.Idle;
        }

        if (currentState == EnemyState.Idle)
        {
            agent.isStopped = true;
        }

        if (currentState == EnemyState.Chase)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
        }

        if (currentState == EnemyState.Attack)
        {
            agent.isStopped = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}