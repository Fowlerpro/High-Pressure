using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 0.05f;
    public float controllerSensitivity = 120f;

    public Transform PlayerBody;

    public float leanAmount = 0.4f;
    public float leanAngle = 12f;
    public float leanSpeed = 8f;

    float xRotation = 0f;

    float currentLean = 0f;

    Vector3 startingLocalPosition;

    public static bool CanMove = true;

    void Start()
    {
        startingLocalPosition = transform.localPosition;
    }

    void Update()
    {
        if (CanMove == false)
            return;

        float lookX = 0f;
        float lookY = 0f;

        if (Mouse.current != null)
        {
            Vector2 mouse =
                Mouse.current.delta.ReadValue();

            lookX += mouse.x * mouseSensitivity;
            lookY += mouse.y * mouseSensitivity;
        }

        if (Gamepad.current != null)
        {
            Vector2 rightStick =
                Gamepad.current.rightStick.ReadValue();

            lookX +=
                rightStick.x *
                controllerSensitivity *
                Time.deltaTime;

            lookY +=
                rightStick.y *
                controllerSensitivity *
                Time.deltaTime;
        }

        xRotation -= lookY;

        xRotation =
            Mathf.Clamp(
                xRotation,
                -90f,
                90f
            );

        PlayerBody.Rotate(
            Vector3.up * lookX
        );

        float targetLean = 0f;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.qKey.isPressed)
            {
                targetLean = -1f;
            }

            if (Keyboard.current.rKey.isPressed)
            {
                targetLean = 1f;
            }
        }

        if (Gamepad.current != null)
        {
            if (Gamepad.current.leftShoulder.isPressed)
            {
                targetLean = -1f;
            }

            if (Gamepad.current.rightShoulder.isPressed)
            {
                targetLean = 1f;
            }
        }

        currentLean =
            Mathf.Lerp(
                currentLean,
                targetLean,
                leanSpeed * Time.deltaTime
            );

        transform.localRotation =
            Quaternion.Euler(
                xRotation,
                0f,
                -currentLean * leanAngle
            );

        Vector3 cameraPosition =
            transform.localPosition;

        cameraPosition.x =
            startingLocalPosition.x +
            currentLean * leanAmount;

        transform.localPosition =
            cameraPosition;
    }
}