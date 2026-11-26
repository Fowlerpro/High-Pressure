using UnityEngine;

//This one handles picking up the new fuse
public class FusePickup : MonoBehaviour
{
    public GameObject offHandFuse;

    private bool collected = false;

    void Start()
    {
        //Hide the offhand fuse until it is nabbed
        offHandFuse.SetActive(false);
    }

    //Called by PlayerInteraction
    public void Interact()
    {
        if (collected)
            return;

        collected = true;

        //Show the fuse in the player's HUD
        offHandFuse.SetActive(true);

        //Remove the physical fuse from the world
        gameObject.SetActive(false);
    }
}
