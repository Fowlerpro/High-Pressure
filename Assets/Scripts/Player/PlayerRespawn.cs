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

    public GameObject[] computers;
    public CompterScreen[] computerScreens;

    CharacterController controller;

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
    }

    IEnumerator RespawnRoutine()
    {
        MouseLook.CanMove = false;

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

        // Reset all computers
        foreach (GameObject computer in computers)
        {
            if (computer != null)
            {
                computer.GetComponent<AccessCodeDisplay>()?.ResetCode();
                computer.GetComponent<ComputerNavi>()?.ResetNavigation();
            }
        }

        // Reset all computer screens
        foreach (CompterScreen screen in computerScreens)
        {
            if (screen != null)
            {
                screen.Logout();
            }
        }

        if (currentSpawn == level2Spawn && enemyLevel2 != null)
        {
            enemyLevel2.ActivateEnemy();
        }

        if (deathPanel != null)
        {
            deathPanel.SetActive(false);
        }

        // Reset all automatic doors in the scene
        DoorDetector[] allDoors = FindObjectsByType<DoorDetector>(
            FindObjectsSortMode.None
        );

        foreach (DoorDetector door in allDoors)
        {
            door.ResetDoor();
        }

        MouseLook.CanMove = true;
    }

    public void SetSpawn(Transform newSpawn)
    {
        currentSpawn = newSpawn;
    }
}