using UnityEngine;

public class TerminalInteract : MonoBehaviour
{
    public Camera playerCamera;
    public Camera TerminalCamera;

    public MonoBehaviour playerMovement;
    public GameObject begonUnityButtons;

    private bool usingTerminal = false;

    void Start()
    {
        TerminalCamera.enabled = false;

        begonUnityButtons.SetActive(false);
    }

    void Update()
    {
        if (usingTerminal && Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape))
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

    void OpenTerminal()
    {
        usingTerminal = true;

        playerCamera.enabled = false;
        TerminalCamera.enabled = true;

        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        begonUnityButtons.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        MouseLook.CanMove = false;
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

        begonUnityButtons.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        MouseLook.CanMove = true;
    }

    //Reference for when Jit gets yo ass
    public void ForceCloseTerminal()
    {
        if (usingTerminal)
        {
            CloseTerminal();
        }
    }
}
