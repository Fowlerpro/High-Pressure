using UnityEngine;

public class TerminalInteract : MonoBehaviour
{
    public Camera playerCamera;
    public Camera TerminalCamera;

    public MonoBehaviour playerMovement;
    public GameObject terminalScreen;

    private bool playerNearby = false;
    private bool usingTerminal = false;

    void Start()
    {
        TerminalCamera.enabled = false;

        if (terminalScreen != null)
        {
            terminalScreen.SetActive(false);
        }
    }

    void Update()
    {
        if (playerNearby && !usingTerminal &&
            Input.GetMouseButtonDown(0))
        {
            OpenTerminal();
        }

        if (usingTerminal && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseTerminal();
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

    void OpenTerminal()
    {
        usingTerminal = true;

        playerCamera.enabled = false;
        TerminalCamera.enabled = true;

        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        if (terminalScreen != null)
        {
            terminalScreen.SetActive(true);
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void CloseTerminal()
    {
        usingTerminal = false;

        TerminalCamera.enabled = false;
        playerCamera.enabled = true;

        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }

        if (terminalScreen != null)
        {
            terminalScreen.SetActive(false);
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
