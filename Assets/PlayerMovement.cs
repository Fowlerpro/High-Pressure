using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private CharacterController controller;
    public float speed = 12f;
   
   
    Vector3 V;
    private void Awake()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MouseLook.CanMove == false) return;
        if (controller.isGrounded && V.y < 0)
        {
            V.y = -2f;
        }
        float X = Input.GetAxis("Horizontal");
        float Z = Input.GetAxis("Vertical");
        Vector3 M = transform.right * X + transform.forward * Z;
        controller.Move(M * speed * Time.deltaTime);
       
    }

}
