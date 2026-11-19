using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;

public class TerminalInterface : MonoBehaviour
{
    public TextMeshProUGUI enteredCodeText;
    public AccessCodeDisplay accessCodeDisplay;

    public GameObject incorrectPanel;
    public GameObject correctPanel;

    private string enteredCode = "";
    private Coroutine feedbackCoroutine;

    public DoorController doorControl;
    public Animator terminalAnimator;

    // Controls whether terminal inputs are allowed
    private bool inputEnabled = false;

    void Start()
    {
        incorrectPanel.SetActive(false);
        correctPanel.SetActive(false);

        enteredCodeText.richText = false;
    }

    void Update()
    {
        // Ignore all inputs until the terminal is opened
        if (!inputEnabled)
            return;

        if (enteredCode.Length < 4)
        {
            // Keyboard controls
            if (Input.GetKeyDown(KeyCode.LeftArrow) ||
                Input.GetKeyDown(KeyCode.A))
                PressSymbol("@");

            else if (Input.GetKeyDown(KeyCode.UpArrow) ||
                     Input.GetKeyDown(KeyCode.W))
                PressSymbol("#");

            else if (Input.GetKeyDown(KeyCode.RightArrow) ||
                     Input.GetKeyDown(KeyCode.D))
                PressSymbol("$");

            else if (Input.GetKeyDown(KeyCode.DownArrow) ||
                     Input.GetKeyDown(KeyCode.S))
                PressSymbol("%");

            // PS5 / Xbox controller D-pad
            if (Gamepad.current != null)
            {
                if (Gamepad.current.dpad.left.wasPressedThisFrame)
                    PressSymbol("@");

                else if (Gamepad.current.dpad.up.wasPressedThisFrame)
                    PressSymbol("#");

                else if (Gamepad.current.dpad.right.wasPressedThisFrame)
                    PressSymbol("$");

                else if (Gamepad.current.dpad.down.wasPressedThisFrame)
                    PressSymbol("%");
            }
        }

        // Clear: Backspace or Circle / B
        if (Input.GetKeyDown(KeyCode.Backspace) ||
            (Gamepad.current != null &&
             Gamepad.current.buttonEast.wasPressedThisFrame))
        {
            ClearCode();
        }

        // Submit: Enter or Cross / A
        if (Input.GetKeyDown(KeyCode.Return) ||
            Input.GetKeyDown(KeyCode.KeypadEnter) ||
            (Gamepad.current != null &&
             Gamepad.current.buttonSouth.wasPressedThisFrame))
        {
            SubmitCode();
        }
    }

    // Called by TerminalInteract
    public void SetInputEnabled(bool enabled)
    {
        inputEnabled = enabled;
    }

    public void PressSymbol(string symbol)
    {
        if (enteredCode.Length < 4)
        {
            enteredCode += symbol;
            UpdateDisplay();
        }
    }

    public void ClearCode()
    {
        enteredCode = "";
        UpdateDisplay();
    }

    public void SubmitCode()
    {
        terminalAnimator.SetTrigger("EnterCode");

        if (enteredCode.Length != 4)
        {
            ShowFeedback(incorrectPanel);
            ClearCode();
            return;
        }

        if (enteredCode == accessCodeDisplay.CurrentCode)
        {
            ShowFeedback(correctPanel);
            doorControl.OpenDoor();
        }
        else
        {
            ShowFeedback(incorrectPanel);
        }

        ClearCode();
    }

    void ShowFeedback(GameObject panel)
    {
        if (feedbackCoroutine != null)
        {
            StopCoroutine(feedbackCoroutine);
        }

        incorrectPanel.SetActive(false);
        correctPanel.SetActive(false);

        panel.SetActive(true);

        feedbackCoroutine = StartCoroutine(HideFeedback(panel));
    }

    IEnumerator HideFeedback(GameObject panel)
    {
        yield return new WaitForSeconds(1f);

        panel.SetActive(false);
        feedbackCoroutine = null;
    }

    void UpdateDisplay()
    {
        enteredCodeText.text = enteredCode.PadRight(4, '_');
    }

    // Terminal button animation trigger stuff
    public void PressTerminalButton(int buttonNumber)
    {
        terminalAnimator.SetTrigger("Button" + buttonNumber + "Pressed");
    }
}
