using UnityEngine;
using UnityEngine.SceneManagement;

//This script detects the player detection on a specific collider, that triggers the win screen while also locking player movement

//It also lets you hook up an onclick to a button that changes to the title screen
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
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}