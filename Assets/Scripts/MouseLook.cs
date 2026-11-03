using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 0.05f;
    public float controllerSensitivity = 120f;

    public Transform PlayerBody;

    float xRotation = 0f;

    public static bool CanMove = false;

    void Update()
    {
        if (CanMove == false)
            return;

        float lookX = 0f;
        float lookY = 0f;

        // Mouse
        if (Mouse.current != null)
        {
            Vector2 mouse = Mouse.current.delta.ReadValue();

            lookX += mouse.x * mouseSensitivity;
            lookY += mouse.y * mouseSensitivity;
        }

        // PS5 right stick
        if (Gamepad.current != null)
        {
            Vector2 rightStick =
                Gamepad.current.rightStick.ReadValue();

            lookX += rightStick.x *
                     controllerSensitivity *
                     Time.deltaTime;

            lookY += rightStick.y *
                     controllerSensitivity *
                     Time.deltaTime;
        }

        xRotation -= lookY;

        xRotation = Mathf.Clamp(
            xRotation,
            -90f,
            90f
        );

        transform.localRotation =
            Quaternion.Euler(xRotation, 0f, 0f);

        PlayerBody.Rotate(
            Vector3.up * lookX
        );
    }
}