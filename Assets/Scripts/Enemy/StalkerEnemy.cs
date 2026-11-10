using UnityEngine;
using UnityEngine.AI;

public class StalkerEnemy : MonoBehaviour
{
    public Transform player;
    public Camera playerCamera;

    public float moveSpeed = 3f;
    public float lookAmount = 0.65f;

    NavMeshAgent agent;

    Vector3 startPosition;
    Quaternion startRotation;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;

        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    void Update()
    {
        if (PlayerLookingAtEnemy())
        {
            agent.isStopped = true;
        }
        else
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
        }
    }

    bool PlayerLookingAtEnemy()
    {
        Vector3 directionToEnemy =
            transform.position - playerCamera.transform.position;

        directionToEnemy.Normalize();

        float looking = Vector3.Dot(
            playerCamera.transform.forward,
            directionToEnemy
        );

        if (looking > lookAmount)
        {
            RaycastHit hit;

            if (Physics.Raycast(
                playerCamera.transform.position,
                directionToEnemy,
                out hit))
            {
                if (hit.transform == transform ||
                    hit.transform.IsChildOf(transform))
                {
                    return true;
                }
            }
        }

        return false;
    }

    public void ResetEnemy()
    {
        agent.Warp(startPosition);

        transform.rotation = startRotation;

        agent.isStopped = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerRespawn respawn =
                other.GetComponent<PlayerRespawn>();

            if (respawn != null)
            {
                respawn.Respawn();
            }
        }
    }
}