using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    public Camera playerCamera;

    public Image reticle;
    public Sprite normalReticle;
    public Sprite interactReticle;

    public float interactDistance = 3f;

    private GameObject currentInteractable;
    private bool isInteracting = false;

    void Update()
    {
        // Don't raycast while using a computer or terminal
        if (isInteracting)
        {
            return;
        }

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                currentInteractable = hit.collider.gameObject;
                reticle.sprite = interactReticle;
            }
            else
            {
                currentInteractable = null;
                reticle.sprite = normalReticle;
            }
        }
        else
        {
            currentInteractable = null;
            reticle.sprite = normalReticle;
        }

        // Interact with the object
        if (currentInteractable != null && Input.GetMouseButtonDown(0))
        {
            currentInteractable.SendMessage("Interact");

            // Hide the reticle only if an interaction actually started
            isInteracting = true;
            reticle.gameObject.SetActive(false);
        }
    }

    public void EndInteraction()
    {
        isInteracting = false;
        reticle.gameObject.SetActive(true);
        reticle.sprite = normalReticle;
    }
}
