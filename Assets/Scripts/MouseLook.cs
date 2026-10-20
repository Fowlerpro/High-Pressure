using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 0.1f;

    public Transform PlayerBody;

    float Xrot = 0f;

    public static bool CanMove = true;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Escape
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            CanMove = !CanMove;

            if (CanMove == false)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale = 1;
            }
        }

        if (CanMove == false)
            return;

        // Mouse movement
        Vector2 mouseMove = Mouse.current.delta.ReadValue();

        float mouseX = mouseMove.x * mouseSensitivity;
        float mouseY = mouseMove.y * mouseSensitivity;

        Xrot -= mouseY;

        Xrot = Mathf.Clamp(Xrot, -90f, 90f);

        transform.localRotation =
            Quaternion.Euler(Xrot, 0f, 0f);

        PlayerBody.Rotate(Vector3.up * mouseX);
    }
}