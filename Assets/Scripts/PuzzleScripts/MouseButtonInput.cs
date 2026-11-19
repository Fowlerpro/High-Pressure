using UnityEngine;
using UnityEngine.UI;

public class MouseButtonInput : MonoBehaviour
{
    public Button targetButton;

    public void ClickTarget()
    {
        targetButton.onClick.Invoke();
    }
}