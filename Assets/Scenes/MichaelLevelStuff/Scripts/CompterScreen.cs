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

    // NEW: Secondary mouse button groups
    [Header("Secondary Mouse Buttons")]
    public GameObject mouseMessages;
    public GameObject mouseFiles;
    public GameObject mouseLoggedIn;
    public GameObject mouseRestricted;

    private bool isLoggedIn = false;

    void Start()
    {
        isLoggedIn = false;

        accessScreen.SetActive(true);
        loggedInScreen.SetActive(false);

        restrictedButton.SetActive(false);
        restrictedScreen.SetActive(false);

        // Make sure all secondary mouse buttons start hidden
        mouseMessages.SetActive(false);
        mouseFiles.SetActive(false);
        mouseLoggedIn.SetActive(false);
        mouseRestricted.SetActive(false);

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

        // Secondary mouse buttons
        mouseMessages.SetActive(true);
        mouseFiles.SetActive(false);
        mouseLoggedIn.SetActive(false);
        mouseRestricted.SetActive(false);

        CloseMessages();
    }

    public void OpenFiles()
    {
        messagesScreen.SetActive(false);
        filesScreen.SetActive(true);
        accessScreen.SetActive(false);
        loggedInScreen.SetActive(false);
        restrictedScreen.SetActive(false);

        // Secondary mouse buttons
        mouseMessages.SetActive(false);
        mouseFiles.SetActive(true);
        mouseLoggedIn.SetActive(false);
        mouseRestricted.SetActive(false);

        CloseFiles();
    }

    public void OpenAcess()
    {
        messagesScreen.SetActive(false);
        filesScreen.SetActive(false);
        accessScreen.SetActive(!isLoggedIn);
        loggedInScreen.SetActive(isLoggedIn);
        restrictedScreen.SetActive(false);

        // Secondary mouse buttons
        mouseMessages.SetActive(false);
        mouseFiles.SetActive(false);
        mouseLoggedIn.SetActive(isLoggedIn);
        mouseRestricted.SetActive(false);

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

            // Secondary mouse buttons
            mouseMessages.SetActive(false);
            mouseFiles.SetActive(false);
            mouseLoggedIn.SetActive(false);
            mouseRestricted.SetActive(true);

            CloseMessages();
        }
    }

    public void Login()
    {
        isLoggedIn = true;

        accessScreen.SetActive(false);
        loggedInScreen.SetActive(true);

        restrictedButton.SetActive(true);

        // Secondary mouse buttons
        mouseLoggedIn.SetActive(true);
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
