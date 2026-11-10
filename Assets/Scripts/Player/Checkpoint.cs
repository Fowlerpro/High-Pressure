using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Transform spawnPoint;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerRespawn respawn =
                other.GetComponent<PlayerRespawn>();

            if (respawn != null)
            {
                respawn.SetSpawn(spawnPoint);
            }
        }
    }
}