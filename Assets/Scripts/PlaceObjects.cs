using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlaceObjects : MonoBehaviour
{
    [SerializeField] private LayerMask _layer;
    [SerializeField] private float _rotateSpeed = 60f;

    Mouse mouse = Mouse.current;
    Keyboard keyboard = Keyboard.current;

    private void Start()
    {
        PositionObjects();
    }

    private void Update()
    {
        PositionObjects();

        if (mouse.leftButton.wasPressedThisFrame)
        {
            gameObject.GetComponent<AutoCarCreate>().enabled = true;
            Destroy(gameObject.GetComponent<PlaceObjects>());
        }

        if (keyboard.zKey.isPressed)
            transform.Rotate(Vector3.up * Time.deltaTime * _rotateSpeed);

        if (keyboard.xKey.isPressed)
            transform.Rotate(Vector3.down * Time.deltaTime * _rotateSpeed);
    }

    private void PositionObjects()
    {
        Ray ray = Camera.main.ScreenPointToRay(mouse.position.ReadValue());
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000f, _layer))
            transform.position = hit.point;
    }

}
