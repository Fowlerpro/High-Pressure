using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections;

public class ScannerButton : MonoBehaviour
{
    public CompterScreen computerScreen;
    public Animator armAnimator;

    public GameObject scannerDone;
    public GameObject scannerIdle;
    public GameObject scannerDoing;

    public ComputerInteract computerInteract;

    public Slider loadingBar;
    public float scanTime = 3f;

    private float holdTime = 0f;
    private bool isHolding = false;
    private bool holdCompleted = false;

    void Start()
    {
        ResetScanner();
    }

    void Update()
    {
        // Only allow scanning when this computer is being used
        if (!computerInteract.IsUsingComputer)
        {
            if (isHolding)
            {
                ReturnToIdle();
            }

            return;
        }

        // Keyboard E or Controller Right Trigger (R2)
        bool holdInput =
            Input.GetKey(KeyCode.E) ||
            (Gamepad.current != null && Gamepad.current.rightTrigger.isPressed);

        bool pressInput =
            Input.GetKeyDown(KeyCode.E) ||
            (Gamepad.current != null && Gamepad.current.rightTrigger.wasPressedThisFrame);

        bool releaseInput =
            Input.GetKeyUp(KeyCode.E) ||
            (Gamepad.current != null && Gamepad.current.rightTrigger.wasReleasedThisFrame);

        // Start scanning
        if (pressInput && !holdCompleted && !isHolding)
        {
            holdTime = 0f;
            isHolding = true;

            loadingBar.value = 0f;

            scannerIdle.SetActive(false);
            scannerDone.SetActive(false);
            scannerDoing.SetActive(true);

            armAnimator.SetTrigger("Reach");
            armAnimator.SetBool("IsHolding", true);
        }

        // Continue scanning while E or R2 is held
        if (isHolding && !holdCompleted && holdInput)
        {
            holdTime += Time.deltaTime;

            loadingBar.value = holdTime / scanTime;

            if (holdTime >= scanTime)
            {
                holdCompleted = true;

                loadingBar.value = 1f;

                scannerDoing.SetActive(false);
                scannerDone.SetActive(true);
            }
        }

        // Stop scanning when E or R2 is released
        if (releaseInput && isHolding)
        {
            isHolding = false;

            armAnimator.SetBool("IsHolding", false);

            if (holdCompleted)
            {
                computerScreen.Login();
            }
            else
            {
                holdTime = 0f;
                loadingBar.value = 0f;

                scannerDoing.SetActive(false);
                scannerIdle.SetActive(true);
            }
        }
    }

    public void ResetScanner()
    {
        holdTime = 0f;
        isHolding = false;
        holdCompleted = false;

        loadingBar.value = 0f;

        scannerDone.SetActive(false);
        scannerDoing.SetActive(false);

        armAnimator.SetBool("IsHolding", false);
    }

    public void ReturnToIdle()
    {
        holdTime = 0f;
        isHolding = false;
        holdCompleted = false;

        loadingBar.value = 0f;

        scannerDone.SetActive(false);
        scannerDoing.SetActive(false);
        scannerIdle.SetActive(true);

        armAnimator.SetBool("IsHolding", false);
    }
}


