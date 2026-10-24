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

                scannerLight.SetActive(true);
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        holdTime = 0f;
        holdCompleted = false;
        isHolding = true;

        scannerLight.SetActive(false);

        armAnimator.SetTrigger("Reach");
        armAnimator.SetBool("IsHolding", true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isHolding = false;

        armAnimator.SetBool("IsHolding", false);

        if (holdCompleted)
        {
            computerScreen.Login();
        }

        scannerLight.SetActive(false);

        holdTime = 0f;
    }
}
