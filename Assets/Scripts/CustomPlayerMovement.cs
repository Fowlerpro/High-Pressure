using UnityEngine;
using UnityEngine.InputSystem;

public class CustomPlayerMovement : MonoBehaviour
{
    CharacterController controller;

    public float speed = 12f;
    public float sprintSpeed = 18f;
    public float crouchSpeed = 6f;
    public float gravity = -20f;

    public Transform playerCamera;

    public float crouchCameraHeight = 0.5f;

    float normalCameraHeight;
    float normalPlayerHeight;
    float yVelocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        normalCameraHeight = playerCamera.localPosition.y;
        normalPlayerHeight = controller.height;
    }

    void Update()
    {
        if (MouseLook.CanMove == false)
            return;

        float moveSpeed = speed;

        // Sprint
        if (Keyboard.current.leftShiftKey.isPressed)
        {
            moveSpeed = sprintSpeed;
        }

        // Crouch
        if (Keyboard.current.leftCtrlKey.isPressed)
        {
            moveSpeed = crouchSpeed;

            controller.height = 1f;

            Vector3 cameraPos = playerCamera.localPosition;
            cameraPos.y = crouchCameraHeight;
            playerCamera.localPosition = cameraPos;
        }
        else
        {
            controller.height = normalPlayerHeight;

            Vector3 cameraPos = playerCamera.localPosition;
            cameraPos.y = normalCameraHeight;
            playerCamera.localPosition = cameraPos;
        }

        // Movement
        float x = 0;
        float z = 0;

        if (Keyboard.current.aKey.isPressed)
            x = -1;

        if (Keyboard.current.dKey.isPressed)
            x = 1;

        if (Keyboard.current.wKey.isPressed)
            z = 1;

        if (Keyboard.current.sKey.isPressed)
            z = -1;

        Vector3 move = transform.right * x + transform.forward * z;

        move = move.normalized;

        controller.Move(move * moveSpeed * Time.deltaTime);

        // Gravity
        if (controller.isGrounded && yVelocity < 0)
        {
            yVelocity = -2f;
        }

        yVelocity += gravity * Time.deltaTime;

        controller.Move(
            Vector3.up * yVelocity * Time.deltaTime
        );
    }
}