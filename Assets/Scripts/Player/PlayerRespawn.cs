using UnityEngine;
using System.Collections;

public class PlayerRespawn : MonoBehaviour
{
    public Transform currentSpawn;

    public Transform level1Spawn;
    public Transform level2Spawn;

    public AIManager enemyLevel1;
    public AIManager enemyLevel2;

    public GameObject deathPanel;

    CharacterController controller;
    
    public TerminalInteract[] terminalInteracts;
    public ComputerInteract[] computerInteracts;
    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (deathPanel != null)
        {
            deathPanel.SetActive(false);
        }
    }

    public void Respawn()
    {
        StartCoroutine(RespawnRoutine());
            
        // Close all terminals
        foreach (TerminalInteract terminal in terminalInteracts)
        {
            if (terminal != null)
            {
                terminal.ForceCloseTerminal();
            }
        }

        // Close all computers
        foreach (ComputerInteract computer in computerInteracts)
        {
            if (computer != null)
            {
                computer.ForceCloseComputer();
            }
        }
    }

    IEnumerator RespawnRoutine()
    {
        if (deathPanel != null)
        {
            deathPanel.SetActive(true);
        }

        yield return new WaitForSeconds(1.5f);

        if (enemyLevel1 != null)
        {
            enemyLevel1.ResetEnemy();
        }

        if (enemyLevel2 != null)
        {
            enemyLevel2.ResetEnemy();
        }

        controller.enabled = false;

        transform.position = currentSpawn.position;

        controller.enabled = true;

        if (currentSpawn == level2Spawn && enemyLevel2 != null)
        {
            enemyLevel2.ActivateEnemy();
        }

        if (deathPanel != null)
        {
            deathPanel.SetActive(false);
        }
    }

    public void SetSpawn(Transform newSpawn)
    {
        currentSpawn = newSpawn;
    }
}