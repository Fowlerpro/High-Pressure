using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//Handles camera switching, player movement and keypad UI activation
public class DialPadInteract : MonoBehaviour
{
    public Camera playerCamera;
    public Camera dialpadCamera;

    public GameObject dialpadCanvas;

    public MonoBehaviour playerMovement;

    public DialPadInterface DialPadInterface;

    public Button firstButton;

    private bool usingDialpad = false;

    void Start()
    {
        dialpadCamera.enabled = false;
        dialpadCanvas.SetActive(false);

        DialPadInterface.SetInputEnabled(false);
    }

    void Update()
    {
        // Exit keypad
        if (usingDialpad &&
            (Input.GetMouseButtonDown(1) ||
             Input.GetKeyDown(KeyCode.Escape) ||
             (Gamepad.current != null &&
              Gamepad.current.buttonNorth.wasPressedThisFrame)))
        {
            CloseKeypad();
        }
    }

    public void Interact()
    {
        if (usingDialpad)
        {
            return;
        }

        OpenKeypad();
    }

    void OpenKeypad()
    {
        usingDialpad = true;

        DialPadInterface.SetInputEnabled(true);

        EventSystem.current.SetSelectedGameObject(firstButton.gameObject);

        playerCamera.enabled = false;
        dialpadCamera.enabled = true;

        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        dialpadCanvas.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        MouseLook.CanMove = false;
    }

    void CloseKeypad()
    {
        usingDialpad = false;

        DialPadInterface.SetInputEnabled(false);

        EventSystem.current.SetSelectedGameObject(null);

        FindAnyObjectByType<PlayerInteraction>().EndInteraction();

        dialpadCamera.enabled = false;
        playerCamera.enabled = true;

        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }

        dialpadCanvas.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        MouseLook.CanMove = true;
    }
}
