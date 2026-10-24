
using UnityEngine;

public class ComputerInteract : MonoBehaviour
{
    public Camera pcPOV;
    public GameObject pcScreen;
    public GameObject player;

    private bool playerNearby = false;
    private bool usingComputer = false;

    private CharacterController playerController;

    void Start()
    {
        // Get the player's CharacterController
        playerController = player.GetComponent<CharacterController>();

        pcPOV.gameObject.SetActive(false);
        pcScreen.SetActive(false);

        // Hide and lock the mouse during normal gameplay
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (playerNearby && !usingComputer && Input.GetMouseButtonDown(0))
        {
            usingComputer = true;

            pcPOV.gameObject.SetActive(true);
            pcScreen.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // Stop the player from moving
            playerController.enabled = false;
        }

        if (usingComputer && Input.GetKeyDown(KeyCode.Escape))
        {
            usingComputer = false;

            pcPOV.gameObject.SetActive(false);
            pcScreen.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // Allow the player to move again
            playerController.enabled = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
        }
    }
}

