using UnityEngine;
using UnityEngine.InputSystem;

public class ComputerInteract : MonoBehaviour
{
    public Camera pcPOV;
    public GameObject mouseCanvas;
    public GameObject player;

    private bool usingComputer = false;

    public bool IsUsingComputer
    {
        get { return usingComputer; }
    }

    private CharacterController playerController;

    void Start()
    {
        playerController = player.GetComponent<CharacterController>();

        pcPOV.gameObject.SetActive(false);
        mouseCanvas.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Exit computer
        if (usingComputer && (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape) || Gamepad.current != null &&
             Gamepad.current.buttonWest.wasPressedThisFrame))
        {
            usingComputer = false;

            FindAnyObjectByType<PlayerInteraction>().EndInteraction();

            pcPOV.gameObject.SetActive(false);
            mouseCanvas.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            MouseLook.CanMove = true;

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
        mouseCanvas.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        MouseLook.CanMove = false;

        playerController.enabled = false;
    }

    public void ForceCloseComputer()
    {
        if (usingComputer)
        {
            usingComputer = false;

            FindAnyObjectByType<PlayerInteraction>().EndInteraction();

            pcPOV.gameObject.SetActive(false);
            mouseCanvas.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            MouseLook.CanMove = true;

            playerController.enabled = true;
        }
    }
}