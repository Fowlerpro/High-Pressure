using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CustomPlayerMovement : MonoBehaviour
{
    CharacterController controller;

    public float speed = 12f;
    public float sprintSpeed = 18f;
    public float crouchSpeed = 6f;
    public float gravity = -20f;

    public Transform playerCamera;
    public Camera cameraObject;

    public float crouchCameraHeight = 0.5f;

    float normalCameraHeight;
    float normalPlayerHeight;
    float yVelocity;

    // Stamina
    public float maxStamina = 100f;
    public float stamina = 100f;
    public float staminaDrain = 25f;
    public float staminaRecharge = 20f;

    public Slider staminaBar;

    bool exhausted = false;

    // FOV
    public float normalFOV = 60f;
    public float sprintFOV = 75f;
    public float fovSpeed = 5f;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        normalCameraHeight = playerCamera.localPosition.y;
        normalPlayerHeight = controller.height;

        stamina = maxStamina;

        staminaBar.maxValue = maxStamina;
        staminaBar.value = stamina;

        cameraObject.fieldOfView = normalFOV;
    }

    void Update()
    {
        if (!controller.enabled)
            return;

        if (MouseLook.CanMove == false)
            return;

        float moveSpeed = speed;

        float x = 0f;
        float z = 0f;

        // Keyboard movement
        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed)
                x = -1;

            if (Keyboard.current.dKey.isPressed)
                x = 1;

            if (Keyboard.current.wKey.isPressed)
                z = 1;

            if (Keyboard.current.sKey.isPressed)
                z = -1;
        }

        // PS5 controller movement
        if (Gamepad.current != null)
        {
            Vector2 leftStick =
                Gamepad.current.leftStick.ReadValue();

            x = leftStick.x;
            z = leftStick.y;
        }

        bool moving = Mathf.Abs(x) > 0.1f ||
                      Mathf.Abs(z) > 0.1f;

        bool sprintPressed = false;
        bool crouchPressed = false;

        // Keyboard buttons
        if (Keyboard.current != null)
        {
            if (Keyboard.current.leftShiftKey.isPressed)
                sprintPressed = true;

            if (Keyboard.current.leftCtrlKey.isPressed)
                crouchPressed = true;
        }

        // PS5 buttons
        if (Gamepad.current != null)
        {
            // L3
            if (Gamepad.current.leftStickButton.isPressed)
                sprintPressed = true;

            // Circle
            if (Gamepad.current.buttonEast.isPressed)
                crouchPressed = true;
        }

        bool sprinting = false;

        // Exhausted
        if (stamina <= 0)
        {
            exhausted = true;
        }

        if (stamina >= 20)
        {
            exhausted = false;
        }

        // Sprint
        if (sprintPressed &&
            moving &&
            exhausted == false &&
            crouchPressed == false)
        {
            moveSpeed = sprintSpeed;
            sprinting = true;

            stamina -= staminaDrain * Time.deltaTime;
        }
        else
        {
            stamina += staminaRecharge * Time.deltaTime;
        }

        stamina = Mathf.Clamp(
            stamina,
            0,
            maxStamina
        );

        staminaBar.value = stamina;

        // Sprint FOV
        if (sprinting)
        {
            cameraObject.fieldOfView = Mathf.Lerp(
                cameraObject.fieldOfView,
                sprintFOV,
                fovSpeed * Time.deltaTime
            );
        }
        else
        {
            cameraObject.fieldOfView = Mathf.Lerp(
                cameraObject.fieldOfView,
                normalFOV,
                fovSpeed * Time.deltaTime
            );
        }

        // Crouch
        if (crouchPressed)
        {
            moveSpeed = crouchSpeed;

            controller.height = 1f;

            Vector3 cameraPos =
                playerCamera.localPosition;

            cameraPos.y = crouchCameraHeight;

            playerCamera.localPosition = cameraPos;
        }
        else
        {
            controller.height = normalPlayerHeight;

            Vector3 cameraPos =
                playerCamera.localPosition;

            cameraPos.y = normalCameraHeight;

            playerCamera.localPosition = cameraPos;
        }

        // Movement
        Vector3 move =
            transform.right * x +
            transform.forward * z;

        move = Vector3.ClampMagnitude(move, 1f);

        controller.Move(
            move * moveSpeed * Time.deltaTime
        );

        // Gravity
        if (controller.isGrounded &&
            yVelocity < 0)
        {
            yVelocity = -2f;
        }

        yVelocity += gravity * Time.deltaTime;

        controller.Move(
            Vector3.up *
            yVelocity *
            Time.deltaTime
        );
    }
}