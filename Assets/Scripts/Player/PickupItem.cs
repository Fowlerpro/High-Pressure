using UnityEngine;
using UnityEngine.InputSystem;

public class PickupItem : MonoBehaviour
{
    public GameObject flashlight;

    bool playerInside = false;

    void Update()
    {
        if (playerInside &&
            Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (flashlight != null)
            {
                flashlight.SetActive(true);
            }

            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
        }
    }
}