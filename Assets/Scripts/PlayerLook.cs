using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    private readonly string AxisMouseX = "Mouse X";
    private readonly string AxisMouseY = "Mouse Y";

    [SerializeField] private float _mouseSensitivity;
    [SerializeField] private Transform _playerBody;

    private float _xRotation = 0f;
    private float _minRotation = -90f;
    private float _maxRotation = 90f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Look();
    }

    private void Look()
    {
        float mouseX = Input.GetAxis(AxisMouseX) * _mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis(AxisMouseY) * _mouseSensitivity * Time.deltaTime;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, _minRotation, _maxRotation);

        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        _playerBody.Rotate(Vector3.up * mouseX);
    }
}
