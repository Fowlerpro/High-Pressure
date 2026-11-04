using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    public GameObject winPanel;

    public MonoBehaviour playerMovement;
    public MonoBehaviour playerLook;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            winPanel.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            playerMovement.enabled = false;
            playerLook.enabled = false;
        }
    }
}