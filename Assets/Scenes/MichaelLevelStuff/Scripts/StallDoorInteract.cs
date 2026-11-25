using UnityEngine;

//This script is short n sweet, it just plays the open or close door animation of the washroom stalls
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
