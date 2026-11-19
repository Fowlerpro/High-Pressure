using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public bool controlsActive = false;
    public GameObject menuPanel;
    public CanvasGroup canvasGroup;
    public float canvasFadeSpeed = 1;
    public GameObject controlsCamera;
    public GameObject mainCamera;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 1f;
    }

    public void StartGame()
    {
        
        if (controlsActive)
        {
            controlsCamera.SetActive(false);
            mainCamera.SetActive(true);
            controlsActive = false;
        }
        StartCoroutine(MenuFade());

    }

    public void ShowControls()
    {
        if (controlsActive == false)
        {
            mainCamera.SetActive(false);
            menuPanel.SetActive(false);
            controlsCamera.SetActive(true);
            controlsActive = true;
        }
        else
        {
            mainCamera.SetActive(true);
            menuPanel.SetActive(true);
            controlsCamera.SetActive(false);
            controlsActive = false;
        }

        }

    public void HideControls()
    {  
        canvasGroup.alpha = 1f;
        menuPanel.SetActive(true);
        controlsCamera.SetActive(false);
        mainCamera.SetActive(true);
        controlsActive = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    private IEnumerator MenuFade()
    {
        while (canvasGroup.alpha > 0f)
        {
            canvasGroup.alpha -= canvasFadeSpeed * Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 0f;
        SceneManager.LoadScene("Greybox Level");
    }
}