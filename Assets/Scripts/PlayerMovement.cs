using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController controller;

    public float speed = 12f;
    public float sprintSpeed = 18f;
    public float crouchSpeed = 6f;

    public Transform playerCamera;

    float normalCameraHeight;
    public float crouchCameraHeight = 0.5f;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        normalCameraHeight = playerCamera.localPosition.y;
    }

    void Update()
    {
        if (MouseLook.CanMove == false)
            return;

        float moveSpeed = speed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = sprintSpeed;
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            moveSpeed = crouchSpeed;

            controller.height = 1f;

            Vector3 cameraPos = playerCamera.localPosition;
            cameraPos.y = crouchCameraHeight;
            playerCamera.localPosition = cameraPos;
        }
        else
        {
            controller.height = 2f;

            Vector3 cameraPos = playerCamera.localPosition;
            cameraPos.y = normalCameraHeight;
            playerCamera.localPosition = cameraPos;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * moveSpeed * Time.deltaTime);
    }
}