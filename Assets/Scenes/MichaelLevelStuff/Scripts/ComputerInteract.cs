using UnityEngine;

public class ComputerInteract : MonoBehaviour
{
    public Camera pcPOV;
    public GameObject player;

    private bool usingComputer = false;

    private CharacterController playerController;

    void Start()
    {
        // Get the player's CharacterController
        playerController = player.GetComponent<CharacterController>();

        pcPOV.gameObject.SetActive(false);

        // Hide and lock the mouse during normal gameplay
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Exit computer
        if (usingComputer && Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape))
        {
            usingComputer = false;

            FindAnyObjectByType<PlayerInteraction>().EndInteraction();

            pcPOV.gameObject.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            MouseLook.CanMove = true;

            // Allow the player to move again
            playerController.enabled = true;
        }
    }
    public void Interact()
    {
        if (usingComputer)
        {
            return;
        }

        usingComputer = true;

        pcPOV.gameObject.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        MouseLook.CanMove = false;

        // Stop the player from moving
        playerController.enabled = false;
    }

    //Reference for Jit if it murks the Player
    public void ForceCloseComputer()
    {
        if (usingComputer)
        {
            usingComputer = false;

            FindAnyObjectByType<PlayerInteraction>().EndInteraction();

            pcPOV.gameObject.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            MouseLook.CanMove = true;

            playerController.enabled = true;
        }
    }
}
