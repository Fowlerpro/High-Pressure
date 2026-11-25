using UnityEngine;
using UnityEngine.UI;

//This script is for the mouse input on the computer interface,
//it baiscally allows for the secondary Unity buttons dedicated for mouse input to "click" on
//the primary Unity button layout.
public class MouseButtonInput : MonoBehaviour
{
    //UI button reference
    public Button targetButton;

    public void ClickTarget()
    {
        //Simulates clicking a Unity button
        targetButton.onClick.Invoke();
    }
}