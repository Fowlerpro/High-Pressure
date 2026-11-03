using UnityEngine;

public class TerminalInteract : MonoBehaviour
{
    public Camera playerCamera;
    public Camera TerminalCamera;

    public MonoBehaviour playerMovement;
    public GameObject terminalScreen;
    public GameObject terminalDial;

    private bool playerNearby = false;
    private bool usingTerminal = false;

    void Start()
    {
        TerminalCamera.enabled = false;

        if (terminalScreen != null)
        {
            terminalScreen.SetActive(false);
        }

        if (terminalDial != null)
        {
            terminalDial.SetActive(false);
        }
    }

    void Update()
    {
        if (usingTerminal && Input.GetMouseButtonDown(1))
        {
            CloseTerminal();
        }
    }
    public void Interact()
    {
        if (usingTerminal)
        {
            return;
        }

        OpenTerminal();
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

        if (terminalDial != null)
        {
            terminalDial.SetActive(true);
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void CloseTerminal()
    {
        usingTerminal = false;

        FindAnyObjectByType<PlayerInteraction>().EndInteraction();

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

        if (terminalDial != null)
        {
            terminalDial.SetActive(false);
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
