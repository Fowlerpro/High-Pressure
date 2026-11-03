using UnityEngine;

public class ComputerInteract : MonoBehaviour
{
    public Camera pcPOV;
    public GameObject pcScreen;
    public GameObject scannerScreen;
    public GameObject player;

    private bool playerNearby = false;
    private bool usingComputer = false;
    private bool canInteract = true;

    private CharacterController playerController;

    void Start()
    {
        // Get the player's CharacterController
        playerController = player.GetComponent<CharacterController>();

        pcPOV.gameObject.SetActive(false);
        pcScreen.SetActive(false);
        scannerScreen.SetActive(false);

        // Hide and lock the mouse during normal gameplay
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Exit computer
        if (usingComputer && Input.GetMouseButtonDown(1))
        {
            usingComputer = false;

            FindAnyObjectByType<PlayerInteraction>().EndInteraction();

            pcPOV.gameObject.SetActive(false);
            pcScreen.SetActive(false);
            scannerScreen.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // Allow the player to move again
            playerController.enabled = true;
        }
    }
    public void Interact()
    {
        if (usingComputer || !canInteract)
        {
            return;
        }

        usingComputer = true;
        canInteract = false;

        pcPOV.gameObject.SetActive(true);
        pcScreen.SetActive(true);
        scannerScreen.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Stop the player from moving
        playerController.enabled = false;
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

            // Allow interaction again after leaving the collider
            canInteract = true;
        }
    }
}
