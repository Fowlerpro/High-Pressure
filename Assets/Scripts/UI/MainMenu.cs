using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public bool controlsActive = false;
    public GameObject controlsPanel;
    public CanvasGroup canvasGroup;
    public float canvasFadeSpeed = 1;

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
            controlsPanel.SetActive(false);
        }
        StartCoroutine(MenuFade());

    }

    public void ShowControls()
    {
        if (controlsActive == false)
        {
        controlsPanel.SetActive(true);
            controlsActive = true;
        }
        else
        {
            controlsPanel.SetActive(false);
            controlsActive = false;
        }

        }

    public void HideControls()
    {
        controlsPanel.SetActive(false);
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