using UnityEngine;
using UnityEngine.AI;

public class AIManager : MonoBehaviour
{
    public Transform player;

    public float normalSpeed = 3f;
    public float aggressiveSpeed = 8f;
    public float aggressiveTime = 10f;

    public float detectionRange = 8f;
    public float viewAngle = 70f;

    public Transform[] patrolPoints;

    NavMeshAgent agent;

    bool active = false;
    bool investigatingPing = false;

    Vector3 pingPosition;

    float aggressiveTimer = 0f;

    Vector3 startPosition;
    Quaternion startRotation;

    int currentPatrolPoint = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        startPosition = transform.position;
        startRotation = transform.rotation;

        agent.speed = normalSpeed;
        agent.isStopped = true;
    }

    void Update()
    {
        if (active == false)
        {
            agent.isStopped = true;
            return;
        }

        if (aggressiveTimer > 0f)
        {
            aggressiveTimer -= Time.deltaTime;
            agent.speed = aggressiveSpeed;
        }
        else
        {
            agent.speed = normalSpeed;
        }

        if (CanSeePlayer())
        {
            investigatingPing = false;

            agent.isStopped = false;
            agent.SetDestination(player.position);

            return;
        }

        if (investigatingPing)
        {
            agent.isStopped = false;
            agent.SetDestination(pingPosition);

            float distance = Vector3.Distance(
                transform.position,
                pingPosition
            );

            if (distance < 1f)
            {
                investigatingPing = false;
            }

            return;
        }

        Patrol();
    }

    void Patrol()
    {
        if (patrolPoints == null || patrolPoints.Length == 0)
        {
            agent.isStopped = true;
            return;
        }

        agent.isStopped = false;

        agent.SetDestination(
            patrolPoints[currentPatrolPoint].position
        );

        float distance = Vector3.Distance(
            transform.position,
            patrolPoints[currentPatrolPoint].position
        );

        if (distance < 1f)
        {
            currentPatrolPoint++;

            if (currentPatrolPoint >= patrolPoints.Length)
            {
                currentPatrolPoint = 0;
            }
        }
    }

    bool CanSeePlayer()
    {
        if (player == null)
        {
            return false;
        }

        Vector3 enemyEyePosition =
            transform.position + Vector3.up;

        Vector3 playerPosition =
            player.position + Vector3.up;

        Vector3 directionToPlayer =
            playerPosition - enemyEyePosition;

        float distanceToPlayer =
            directionToPlayer.magnitude;

        if (distanceToPlayer > detectionRange)
        {
            return false;
        }

        directionToPlayer.Normalize();

        float angleToPlayer =
            Vector3.Angle(
                transform.forward,
                directionToPlayer
            );

        if (angleToPlayer > viewAngle / 2f)
        {
            return false;
        }

        RaycastHit hit;

        if (Physics.Raycast(
            enemyEyePosition,
            directionToPlayer,
            out hit,
            detectionRange))
        {
            if (hit.transform.CompareTag("Player"))
            {
                return true;
            }

            if (hit.transform.IsChildOf(player))
            {
                return true;
            }
        }

        return false;
    }

    public void HearNoise(Vector3 position)
    {
        active = true;

        pingPosition = position;
        investigatingPing = true;

        aggressiveTimer = aggressiveTime;
        agent.speed = aggressiveSpeed;

        agent.isStopped = false;
        agent.SetDestination(pingPosition);
    }

    public void ActivateEnemy()
    {
        active = true;

        agent.speed = normalSpeed;
        agent.isStopped = false;
    }

    public void ResetEnemy()
    {
        active = false;
        investigatingPing = false;

        aggressiveTimer = 0f;

        currentPatrolPoint = 0;

        agent.isStopped = true;
        agent.ResetPath();

        agent.Warp(startPosition);

        transform.rotation = startRotation;

        agent.speed = normalSpeed;
        agent.isStopped = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 direction =
                other.transform.position - transform.position;

            direction.y = 0f;

            if (direction != Vector3.zero)
            {
                transform.rotation =
                    Quaternion.LookRotation(direction);
            }

            PlayerRespawn respawn =
                other.GetComponent<PlayerRespawn>();

            if (respawn != null)
            {
                respawn.Respawn();
            }
        }
    }
}