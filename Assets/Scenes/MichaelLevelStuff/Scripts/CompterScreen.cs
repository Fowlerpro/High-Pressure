using UnityEngine;

public class CompterScreen : MonoBehaviour
{
    public GameObject messagesScreen;
    public GameObject filesScreen;
    public GameObject accessScreen;

    public GameObject loggedInScreen;

    public GameObject restrictedButton;
    public GameObject restrictedScreen;

    // Individual message panels
    public GameObject message1;
    public GameObject message2;
    public GameObject message3;

    // Individual file panels
    public GameObject file1;
    public GameObject file2;

    public ScannerButton scannerButton;

    private bool isLoggedIn = false;

    void Start()
    {
        isLoggedIn = false;

        accessScreen.SetActive(true);
        loggedInScreen.SetActive(false);

        restrictedButton.SetActive(false);
        restrictedScreen.SetActive(false);

        // Make sure all individual messages start hidden
        CloseMessages();
    }

    public void OpenMessages()
    {
        messagesScreen.SetActive(true);
        filesScreen.SetActive(false);
        accessScreen.SetActive(false);
        loggedInScreen.SetActive(false);
        restrictedScreen.SetActive(false);

        CloseMessages();
    }

    public void OpenFiles()
    {
        messagesScreen.SetActive(false);
        filesScreen.SetActive(true);
        accessScreen.SetActive(false);
        loggedInScreen.SetActive(false);
        restrictedScreen.SetActive(false);

        CloseFiles();
    }

    public void OpenAcess()
    {
        messagesScreen.SetActive(false);
        filesScreen.SetActive(false);
        accessScreen.SetActive(!isLoggedIn);
        loggedInScreen.SetActive(isLoggedIn);
        restrictedScreen.SetActive(false);

        CloseMessages();
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

            CloseMessages();
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

        scannerButton.ResetScanner();

        OpenAcess();
    }

    // Open individual messages
    public void OpenMessage1()
    {
        CloseMessages();
        message1.SetActive(true);
    }

    public void OpenMessage2()
    {
        CloseMessages();
        message2.SetActive(true);
    }

    public void OpenMessage3()
    {
        CloseMessages();
        message3.SetActive(true);
    }

    // Close all individual messages
    public void CloseMessages()
    {
        message1.SetActive(false);
        message2.SetActive(false);
        message3.SetActive(false);
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

            CloseMessages();
        }
    }
    public void OpenFile1()
    {
        CloseFiles();
        file1.SetActive(true);
    }

    public void OpenFile2()
    {
        CloseFiles();
        file2.SetActive(true);
    }

    public void CloseFiles()
    {
        file1.SetActive(false);
        file2.SetActive(false);
    }
}
