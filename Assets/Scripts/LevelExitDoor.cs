using UnityEngine;

public class LevelExitDoor : MonoBehaviour
{
    public Transform leftDoor;
    public Transform rightDoor;

    public float openAngle = 90f;
    public float speed = 3f;

    bool open = false;

    Quaternion leftClosed;
    Quaternion rightClosed;

    Quaternion leftOpen;
    Quaternion rightOpen;

    void Start()
    {
        leftClosed = leftDoor.localRotation;
        rightClosed = rightDoor.localRotation;

        leftOpen = leftClosed * Quaternion.Euler(0, -openAngle, 0);
        rightOpen = rightClosed * Quaternion.Euler(0, openAngle, 0);
    }

    void Update()
    {
        if (open)
        {
            leftDoor.localRotation = Quaternion.Lerp(
                leftDoor.localRotation,
                leftOpen,
                speed * Time.deltaTime
            );

            rightDoor.localRotation = Quaternion.Lerp(
                rightDoor.localRotation,
                rightOpen,
                speed * Time.deltaTime
            );
        }
        else
        {
            leftDoor.localRotation = Quaternion.Lerp(
                leftDoor.localRotation,
                leftClosed,
                speed * Time.deltaTime
            );

            rightDoor.localRotation = Quaternion.Lerp(
                rightDoor.localRotation,
                rightClosed,
                speed * Time.deltaTime
            );
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            open = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            open = false;
        }
    }
}
