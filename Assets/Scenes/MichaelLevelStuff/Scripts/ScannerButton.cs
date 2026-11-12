using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScannerButton : MonoBehaviour
{
    public CompterScreen computerScreen;
    public Animator armAnimator;

    public GameObject scannerDone;
    public GameObject scannerIdle;
    public GameObject scannerDoing;

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
        // Start scanning when E is pressed
        if (Input.GetKeyDown(KeyCode.E) && !holdCompleted && !isHolding)
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

        // Continue scanning while E is held
        if (isHolding && !holdCompleted)
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

        // Stop scanning when E is released
        if (Input.GetKeyUp(KeyCode.E) && isHolding)
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
        scannerIdle.SetActive(true);

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

