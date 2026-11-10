using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class ScannerButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
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

    private Coroutine scanDoneCoroutine;

    void Start()
    {
        ResetScanner();
    }

    void Update()
    {
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

                scanDoneCoroutine = StartCoroutine(ShowScanDone());
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (holdCompleted)
        {
            return;
        }

        holdTime = 0f;
        isHolding = true;

        loadingBar.value = 0f;

        scannerIdle.SetActive(false);
        scannerDone.SetActive(false);
        scannerDoing.SetActive(true);

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
        else
        {
            holdTime = 0f;
            loadingBar.value = 0f;

            scannerDoing.SetActive(false);
            scannerIdle.SetActive(true);
        }
    }

    IEnumerator ShowScanDone()
    {
        yield return new WaitForSeconds(1f);

        scannerDone.SetActive(false);
        scannerIdle.SetActive(true);

        scanDoneCoroutine = null;
    }

    public void ResetScanner()
    {
        if (scanDoneCoroutine != null)
        {
            StopCoroutine(scanDoneCoroutine);
            scanDoneCoroutine = null;
        }

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
