using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 10.0f, _speed = 10.0f, _zoomSpeed = 10.0f, _mult = 1f;

    private void Update()
    {
        Keyboard keyboard = Keyboard.current;
        Mouse mouse = Mouse.current;

        float rotate = 0f;
        float hor = keyboard.aKey.isPressed ? -1f : keyboard.dKey.isPressed ? 1f : 0f;
        float ver = keyboard.sKey.isPressed ? -1f : keyboard.wKey.isPressed ? 1f : 0f;
        _mult = keyboard.leftShiftKey.isPressed ? 2f : 1f;

        if (keyboard.qKey.isPressed)
            rotate = -1f;
        else if (keyboard.eKey.isPressed)
            rotate = 1f;

        transform.Rotate(Vector3.up * _rotationSpeed * Time.deltaTime * rotate * _mult, Space.World);
        transform.Translate(new Vector3(hor, 0, ver) * Time.deltaTime * _mult * _speed, Space.Self);
        transform.position += transform.up * _zoomSpeed * Time.deltaTime * mouse.scroll.ReadValue().y * _mult;
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -20f, 30f), transform.position.z);
    }
}
