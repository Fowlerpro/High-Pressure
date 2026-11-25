using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//This one handles keypad buttons, code input and controller/keyboard controls
public class DialPadInterface : MonoBehaviour
{
    public TextMeshProUGUI enteredCodeText;

    // The fixed code for opening the utility closet
    public string correctCode = "4826";

    private string enteredCode = "";

    private bool inputEnabled = false;

    void Start()
    {
        enteredCodeText.richText = false;
        UpdateDisplay();
    }

    void Update()
    {
        if (!inputEnabled)
            return;

        // Keyboard number input
        if (enteredCode.Length < 4)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
                PressNumber(1);

            else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
                PressNumber(2);

            else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
                PressNumber(3);

            else if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
                PressNumber(4);

            else if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))
                PressNumber(5);

            else if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6))
                PressNumber(6);

            else if (Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7))
                PressNumber(7);

            else if (Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Keypad8))
                PressNumber(8);

            else if (Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Keypad9))
                PressNumber(9);
        }

        // Clear: Backspace or Controller East (B / Circle)
        if (Input.GetKeyDown(KeyCode.Backspace) ||
            (Gamepad.current != null &&
             Gamepad.current.buttonEast.wasPressedThisFrame))
        {
            ClearCode();
        }

        // Submit: Enter or Controller South (A / Cross)
        if (Input.GetKeyDown(KeyCode.Return) ||
            Input.GetKeyDown(KeyCode.KeypadEnter) ||
            (Gamepad.current != null &&
             Gamepad.current.buttonSouth.wasPressedThisFrame))
        {
            SubmitCode();
        }
    }

    // Enables or disables keypad input
    public void SetInputEnabled(bool enabled)
    {
        inputEnabled = enabled;
    }

    // Adds a number to the entered code
    public void PressNumber(int number)
    {
        if (!inputEnabled || enteredCode.Length >= 4)
            return;

        enteredCode += number.ToString();

        UpdateDisplay();
    }

    // Clears the entered code
    public void ClearCode()
    {
        enteredCode = "";

        UpdateDisplay();
    }

    // Checks whether the entered code is correct
    public void SubmitCode()
    {
        if (!inputEnabled)
            return;

        if (enteredCode == correctCode)
        {
            Debug.Log("Correct code!");
        }
        else
        {
            Debug.Log("Incorrect code!");
        }

        ClearCode();
    }

    // Updates the keypad display
    void UpdateDisplay()
    {
        enteredCodeText.text = enteredCode.PadRight(4, '_');
    }
}
