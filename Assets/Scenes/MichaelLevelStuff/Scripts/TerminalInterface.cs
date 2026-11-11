using UnityEngine;
using TMPro;
using System.Collections;

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

    void Start()
    {
        incorrectPanel.SetActive(false);
        correctPanel.SetActive(false);

        // Display symbols as normal text
        enteredCodeText.richText = false;
    }

    void Update()
    {
        // Arrow keys represent the four symbols
        if (enteredCode.Length < 4)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                PressSymbol("@");

            if (Input.GetKeyDown(KeyCode.UpArrow))
                PressSymbol("#");

            if (Input.GetKeyDown(KeyCode.RightArrow))
                PressSymbol("$");

            if (Input.GetKeyDown(KeyCode.DownArrow))
                PressSymbol("%");
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            ClearCode();
        }

        if (Input.GetKeyDown(KeyCode.Return) ||
            Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SubmitCode();
        }
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
}
