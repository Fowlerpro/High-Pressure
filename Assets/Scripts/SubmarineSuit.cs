using UnityEngine;

public class SubmarineSuit : MonoBehaviour
{
    public GameObject suitLight;

    private bool playerClose;

    void Update()
    {
        if (playerClose && Input.GetKeyDown(KeyCode.E))
        {
            suitLight.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerClose = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerClose = false;
        }
    }
}