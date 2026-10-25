using UnityEngine;

public class DoorDetector : MonoBehaviour
{
    public bool someoneNear = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") ||
            other.CompareTag("Enemy"))
        {
            someoneNear = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") ||
            other.CompareTag("Enemy"))
        {
            someoneNear = false;
        }
    }
}