using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class AIManager : MonoBehaviour
{
    public Transform player;
    public Transform[] patrolPoints;

    public float moveSpeed = 3f;
    public float chaseDistance = 10f;
    public float attackDistance = 2f;
    public float searchTime = 3f;

    NavMeshAgent agent;

    int patrolIndex = 0;

    Vector3 lastKnownPosition;
    float searchTimer = 0f;

    enum EnemyState
    {
        Patrol,
        Chase,
        Search,
        Attack
    }

    EnemyState currentState;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.speed = moveSpeed;
        agent.stoppingDistance = attackDistance;

        currentState = EnemyState.Patrol;

        GoToNextPatrolPoint();
    }

    void Update()
    {
        float distance = Vector3.Distance(
            transform.position,
            player.position
        );

        bool canSeePlayer = CanSeePlayer();

        if (distance <= attackDistance && canSeePlayer)
        {
            currentState = EnemyState.Attack;
        }
        else if (distance <= chaseDistance && canSeePlayer)
        {
            currentState = EnemyState.Chase;

            lastKnownPosition = player.position;
        }

        if (currentState == EnemyState.Patrol)
        {
            Patrol();
        }

        if (currentState == EnemyState.Chase)
        {
            Chase();
        }

        if (currentState == EnemyState.Search)
        {
            Search();
        }

        if (currentState == EnemyState.Attack)
        {
            Attack();
        }
    }

    void Patrol()
    {
        agent.isStopped = false;

        if (!agent.pathPending &&
            agent.remainingDistance < 0.5f)
        {
            GoToNextPatrolPoint();
        }
    }

    void Chase()
    {
        agent.isStopped = false;
        agent.SetDestination(player.position);

        if (CanSeePlayer())
        {
            lastKnownPosition = player.position;
        }
        else
        {
            currentState = EnemyState.Search;

            searchTimer = searchTime;

            agent.SetDestination(lastKnownPosition);
        }
    }

    void Search()
    {
        agent.isStopped = false;

        agent.SetDestination(lastKnownPosition);

        searchTimer -= Time.deltaTime;

        if (CanSeePlayer())
        {
            currentState = EnemyState.Chase;
            return;
        }

        if (searchTimer <= 0)
        {
            currentState = EnemyState.Patrol;

            GoToNextPatrolPoint();
        }
    }

    void Attack()
    {
        agent.isStopped = true;

        float distance = Vector3.Distance(
            transform.position,
            player.position
        );

        if (!CanSeePlayer())
        {
            currentState = EnemyState.Search;

            searchTimer = searchTime;
            return;
        }

        if (distance > attackDistance)
        {
            currentState = EnemyState.Chase;
        }
    }

    bool CanSeePlayer()
    {
        Vector3 direction = player.position - transform.position;

        RaycastHit hit;

        if (Physics.Raycast(
            transform.position,
            direction.normalized,
            out hit,
            chaseDistance))
        {
            if (hit.transform.CompareTag("Player"))
            {
                return true;
            }
        }

        return false;
    }

    void GoToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0)
            return;

        agent.SetDestination(
            patrolPoints[patrolIndex].position
        );

        patrolIndex++;

        if (patrolIndex >= patrolPoints.Length)
        {
            patrolIndex = 0;
        }
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