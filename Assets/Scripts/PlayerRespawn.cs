using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public Transform currentSpawn;

    public Transform level2Spawn;

    public StalkerEnemy stalkerEnemy;
    public AIManager enemy2;

    CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    public void Respawn()
    {
        controller.enabled = false;

        transform.position = currentSpawn.position;

        controller.enabled = true;

        if (stalkerEnemy != null)
        {
            stalkerEnemy.ResetEnemy();
        }

        if (enemy2 != null)
        {
            enemy2.ResetEnemy();

            if (currentSpawn == level2Spawn)
            {
                enemy2.ActivateEnemy();
            }
        }
    }

    public void SetSpawn(Transform newSpawn)
    {
        currentSpawn = newSpawn;
    }
}