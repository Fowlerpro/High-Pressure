using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class AIManager : MonoBehaviour
{
    public Transform player;

    public float normalSpeed = 3f;
    public float aggressiveSpeed = 8f;

    NavMeshAgent agent;

    bool active = false;

    Coroutine aggressiveCoroutine;

    Vector3 startPosition;
    Quaternion startRotation;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.speed = normalSpeed;
        agent.isStopped = true;

        startPosition = transform.position;
        startRotation = transform.rotation;
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

        agent.isStopped = false;
        agent.speed = normalSpeed;

        Debug.Log("Enemy 2 activated");
    }

    public void BecomeAggressive()
    {
        active = true;

        if (aggressiveCoroutine != null)
        {
            StopCoroutine(aggressiveCoroutine);
        }

        aggressiveCoroutine = StartCoroutine(AggressiveTime());
    }

    IEnumerator AggressiveTime()
    {
        agent.speed = aggressiveSpeed;

        yield return new WaitForSeconds(10f);

        agent.speed = normalSpeed;

        aggressiveCoroutine = null;
    }

    public void ResetEnemy()
    {
        if (aggressiveCoroutine != null)
        {
            StopCoroutine(aggressiveCoroutine);
            aggressiveCoroutine = null;
        }

        agent.Warp(startPosition);

        transform.rotation = startRotation;

        agent.speed = normalSpeed;

        active = false;
        agent.isStopped = true;
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