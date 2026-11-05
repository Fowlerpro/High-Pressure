using UnityEngine;

public class DoorDetector : MonoBehaviour
{
    public bool someoneNear = false;

    public Animator doorAnimator;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") ||
            other.CompareTag("Enemy"))
        {
            someoneNear = true;

            doorAnimator.SetTrigger("OpenDoor");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") ||
            other.CompareTag("Enemy"))
        {
            someoneNear = false;

            doorAnimator.SetTrigger("CloseDoor");
        }
    }
}