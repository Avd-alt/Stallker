using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private float _horizontalTurnSensitivity;
    [SerializeField] private float _verticalTurnSensitivity;
    [SerializeField] private float _verticalMinAngel = -89;
    [SerializeField] private float _verticalMaxAngel = 89;

    private PlayerInput _playerInput;
    private float _cameraAngle = 0;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _cameraAngle = _cameraTransform.localEulerAngles.x;
    }

    private void Update()
    {
        RotateCamera();
    }

    private void RotateCamera()
    {
        _cameraAngle -= _playerInput.MouseYDirection * _verticalTurnSensitivity;
        _cameraAngle = Mathf.Clamp(_cameraAngle, _verticalMinAngel, _verticalMaxAngel);
        _cameraTransform.localEulerAngles = Vector3.right * _cameraAngle;

        transform.Rotate(Vector3.up * _horizontalTurnSensitivity * _playerInput.MouseXDirection);
    }
}
