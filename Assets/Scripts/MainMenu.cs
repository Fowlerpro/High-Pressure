using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject menuCanvas;
    public GameObject controlsPanel;

    void Start()
    {
        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        MouseLook.CanMove = false;

        controlsPanel.SetActive(false);
    }

    public void StartGame()
    {
        menuCanvas.SetActive(false);

        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        MouseLook.CanMove = true;
    }

    public void ShowControls()
    {
        controlsPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}