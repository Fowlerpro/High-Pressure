using UnityEngine;

public class StallDoorInteract : MonoBehaviour
{
    public Animator doorAnimator;

    private bool isOpen = false;

    public void Interact()
    {
        if (isOpen == false)
        {
            doorAnimator.SetTrigger("OpenStall");
            isOpen = true;
        }
        else
        {
            doorAnimator.SetTrigger("CloseStall");
            isOpen = false;
        }
    }
}
