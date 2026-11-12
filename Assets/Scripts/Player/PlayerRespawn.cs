using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public Transform currentSpawn;

    public Transform level1Spawn;
    public Transform level2Spawn;

    public AIManager enemyLevel1;
    public AIManager enemyLevel2;

    public TerminalInteract terminalInteract;
    public ComputerInteract computerInteract;

    CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    public void Respawn()
    {
        //Reset terminal if it is open
        if (terminalInteract != null)
        {
            terminalInteract.ForceCloseTerminal();
        }

        //Reset computer if its open
        if (computerInteract != null)
        {
            computerInteract.ForceCloseComputer();
        }

        // Reset BOTH enemies
        if (enemyLevel1 != null)
        {
            enemyLevel1.ResetEnemy();
        }

        if (enemyLevel2 != null)
        {
            enemyLevel2.ResetEnemy();
        }

        // Respawn player
        controller.enabled = false;

        transform.position = currentSpawn.position;

        controller.enabled = true;

        // If already in Level 2,
        // reactivate the Level 2 enemy
        if (currentSpawn == level2Spawn)
        {
            enemyLevel2.ActivateEnemy();
        }
    }

    public void SetSpawn(Transform newSpawn)
    {
        currentSpawn = newSpawn;
    }
}