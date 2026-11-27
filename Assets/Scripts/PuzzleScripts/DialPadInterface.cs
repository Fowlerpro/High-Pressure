using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;

//Handles dial pad buttons, code input and correct/incorrect feedback
public class DialPadInterface : MonoBehaviour
{
    public TextMeshProUGUI enteredCodeText;

    // Fixed code for opening the utility closet
    public string correctCode = "4826";

    // Correct and incorrect feedback lights
    public GameObject correctLight;
    public GameObject incorrectLight;

    public float feedbackTime = 1f;

    private string enteredCode = "";

    //Reference to the utility closet door script
    public UtilityDoors utilityDoors;

    public Animator dialPadAnimator;

    private bool inputEnabled = false;

    private Coroutine feedbackCoroutine;

    void Start()
    {
        enteredCodeText.richText = false;

        correctLight.SetActive(false);
        incorrectLight.SetActive(false);

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

        // Submit: Enter
        if (Input.GetKeyDown(KeyCode.Return) ||
            Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SubmitCode();
        }
    }

    // Enables or disables dial pad input
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
            utilityDoors.UnlockAndOpen();

            ShowLight(correctLight);

            // Door opening will be added later
        }
        else
        {
            ShowLight(incorrectLight);
        }

        ClearCode();
    }

    // Activates the correct feedback light
    void ShowLight(GameObject light)
    {
        if (feedbackCoroutine != null)
        {
            StopCoroutine(feedbackCoroutine);
        }

        correctLight.SetActive(false);
        incorrectLight.SetActive(false);

        light.SetActive(true);

        feedbackCoroutine = StartCoroutine(HideLights());
    }

    // Turns off feedback lights after a short delay
    IEnumerator HideLights()
    {
        yield return new WaitForSeconds(feedbackTime);

        correctLight.SetActive(false);
        incorrectLight.SetActive(false);

        feedbackCoroutine = null;
    }

    // Updates the dial pad display
    void UpdateDisplay()
    {
        enteredCodeText.text = enteredCode.PadRight(4, '_');
    }

    // Plays the animation for the selected dial pad button
    public void PlayButtonAnimation(string animationName)
    {
        dialPadAnimator.SetTrigger(animationName);
    }
}
