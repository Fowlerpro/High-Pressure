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

    void Start()
    {
        incorrectPanel.SetActive(false);
        correctPanel.SetActive(false);
    }

    void Update()
    {
        if (enteredCode.Length < 4)
        {
            for (int i = 0; i <= 9; i++)
            {
                if (Input.GetKeyDown(KeyCode.Alpha0 + i) ||
                    Input.GetKeyDown(KeyCode.Keypad0 + i))
                {
                    PressNumber(i.ToString());
                }
            }
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

    public void PressNumber(string number)
    {
        if (enteredCode.Length < 4)
        {
            enteredCode += number;
            UpdateDisplay();
        }
    }

    public void ClearCode()
    {
        enteredCode = "";
        UpdateDisplay();
    }

    void SubmitCode()
    {
        if (enteredCode.Length != 4)
        {
            return;
        }

        if (enteredCode == accessCodeDisplay.CurrentCode)
        {
            Debug.Log("Correct");
            ShowFeedback(correctPanel);

            doorControl.OpenDoor();
        }
        else
        {
            Debug.Log("Incorrect");
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
