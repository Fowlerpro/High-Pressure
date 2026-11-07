using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public Transform currentSpawn;

    public Transform level1Spawn;
    public Transform level2Spawn;

    public StalkerEnemy stalkerEnemy;

    public AIManager enemy2;
    public AIManager enemy2FirstArea;

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

        // Reset Enemy 1
        if (stalkerEnemy != null)
        {
            stalkerEnemy.ResetEnemy();
        }

        // Reset Enemy 2 in Level 2
        if (enemy2 != null)
        {
            enemy2.ResetEnemy();

            if (currentSpawn == level2Spawn)
            {
                enemy2.ActivateEnemy();
            }
        }

        // Reset duplicated Enemy 2 in Level 1
        if (enemy2FirstArea != null)
        {
            enemy2FirstArea.ResetEnemy();

            if (currentSpawn == level1Spawn)
            {
                enemy2FirstArea.ActivateEnemy();
            }
        }
    }

    public void SetSpawn(Transform newSpawn)
    {
        currentSpawn = newSpawn;
    }
}