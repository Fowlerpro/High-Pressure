using UnityEngine;
using UnityEngine.EventSystems;

public class ScannerButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public CompterScreen computerScreen;
    public Animator armAnimator;

    public GameObject scannerLight;

    private float holdTime = 0f;
    private bool isHolding = false;
    private bool holdCompleted = false;

    void Start()
    {
        scannerLight.SetActive(false);
    }

    void Update()
    {
        if (isHolding)
        {
            holdTime += Time.deltaTime;

            if (holdTime >= 3f && !holdCompleted)
            {
                holdCompleted = true;

                // Turn on the scanner light after 3 seconds
                scannerLight.SetActive(true);
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        holdTime = 0f;
        holdCompleted = false;
        isHolding = true;

        // Turn off the light when starting a new scan
        scannerLight.SetActive(false);

        // Start the arm reaching animation
        armAnimator.SetTrigger("Reach");
        armAnimator.SetBool("IsHolding", true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isHolding = false;

        // Make the arm retreat
        armAnimator.SetBool("IsHolding", false);

        // Log in only if the player held for 3 seconds
        if (holdCompleted)
        {
            computerScreen.Login();
        }

        // Turn off the scanner light when released
        scannerLight.SetActive(false);

        holdTime = 0f;
    }
}
