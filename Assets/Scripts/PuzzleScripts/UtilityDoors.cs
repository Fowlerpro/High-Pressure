using UnityEngine;

// Controls the utility closet door using its Animator
public class UtilityDoors : MonoBehaviour
{
    public Animator doorAnimator;
    public Transform doorPoint;

    public float openDistance = 1.5f;
    public float awayDistance = 2.5f;

    private bool doorOpen = false;
    private bool entryUnlocked = false;
    private bool playerInside = false;
    private bool hasMovedAwayFromDoor = false;

    private Transform player;

    // Opens the door when the correct code is entered
    public void UnlockAndOpen()
    {
        entryUnlocked = true;
        OpenDoor();
    }

    private void Update()
    {
        if (!playerInside || player == null)
            return;

        float distance = Vector3.Distance(
            player.position,
            doorPoint.position
        );

        // Check if the player has moved away from the doorway
        if (distance >= awayDistance)
        {
            hasMovedAwayFromDoor = true;
        }

        // Open the door when the player approaches it from inside
        if (hasMovedAwayFromDoor && distance <= openDistance)
        {
            OpenDoor();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        // Only allow entry after the correct code is entered
        if (entryUnlocked)
        {
            player = other.transform;
            playerInside = true;
            entryUnlocked = false;
            hasMovedAwayFromDoor = false;

            CloseDoor();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (playerInside)
        {
            playerInside = false;
            hasMovedAwayFromDoor = false;
            player = null;

            // Lock the door again after leaving
            CloseDoor();
        }
    }

    private void OpenDoor()
    {
        if (doorOpen)
            return;

        doorOpen = true;
        doorAnimator.SetTrigger("OpenDoor");
    }

    private void CloseDoor()
    {
        if (!doorOpen)
            return;

        doorOpen = false;
        doorAnimator.SetTrigger("CloseDoor");
    }
}
