using UnityEngine;

public class CompterScreen : MonoBehaviour
{
    public GameObject messagesScreen;
    public GameObject filesScreen;
    public GameObject accessScreen;

    // Screen shown after logging in
    public GameObject loggedInScreen;

    // Restricted feature
    public GameObject restrictedButton;
    public GameObject restrictedScreen;

    private bool isLoggedIn = false;

    void Start()
    {
        // Start logged out
        isLoggedIn = false;

        accessScreen.SetActive(true);
        loggedInScreen.SetActive(false);

        restrictedButton.SetActive(false);
        restrictedScreen.SetActive(false);
    }

    public void OpenMessages()
    {
        messagesScreen.SetActive(true);
        filesScreen.SetActive(false);
        accessScreen.SetActive(false);
        loggedInScreen.SetActive(false);
        restrictedScreen.SetActive(false);
    }

    public void OpenFiles()
    {
        messagesScreen.SetActive(false);
        filesScreen.SetActive(true);
        accessScreen.SetActive(false);
        loggedInScreen.SetActive(false);
        restrictedScreen.SetActive(false);
    }

    public void OpenAcess()
    {
        messagesScreen.SetActive(false);
        filesScreen.SetActive(false);
        accessScreen.SetActive(!isLoggedIn);
        loggedInScreen.SetActive(isLoggedIn);
        restrictedScreen.SetActive(false);
    }

    public void OpenRestrictedScreen()
    {
        if (isLoggedIn)
        {
            messagesScreen.SetActive(false);
            filesScreen.SetActive(false);
            accessScreen.SetActive(false);
            loggedInScreen.SetActive(false);
            restrictedScreen.SetActive(true);
        }
    }

    public void Login()
    {
        isLoggedIn = true;

        accessScreen.SetActive(false);
        loggedInScreen.SetActive(true);

        restrictedButton.SetActive(true);
    }

    public void Logout()
    {
        isLoggedIn = false;

        accessScreen.SetActive(true);
        loggedInScreen.SetActive(false);

        restrictedButton.SetActive(false);
        restrictedScreen.SetActive(false);

        // Return to the Access tab
        OpenAcess();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            messagesScreen.SetActive(false);
            filesScreen.SetActive(false);
            accessScreen.SetActive(false);
            loggedInScreen.SetActive(false);
            restrictedScreen.SetActive(false);
        }
    }
}

