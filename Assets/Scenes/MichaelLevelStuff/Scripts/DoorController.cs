using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Animator doorAnimator;

    private bool triggered = false;

    public void OpenDoor()
    {
        triggered = false;
        doorAnimator.SetTrigger("OpenDoor");
    }

    public void CloseDoor()
    {
        doorAnimator.SetTrigger("CloseDoor");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !triggered)
        {
            // Only close the door after it reaches its open idle state
            if (doorAnimator.GetCurrentAnimatorStateInfo(0).IsName("DoorOpenIdle"))
            {
                triggered = true;
                CloseDoor();
            }
        }
    }
}
