using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _strafeSpeed;
    [SerializeField] private Transform _cameraTransform;

    private PlayerInput _playerInput;
    private CharacterController _characterController;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 forward = Vector3.ProjectOnPlane(_cameraTransform.forward, Vector3.up).normalized;
        Vector3 right = Vector3.ProjectOnPlane(_cameraTransform.right, Vector3.up).normalized;

        Vector3 moveDirection = forward * _playerInput.VerticalDirection * _speed + right * _playerInput.HorizontalDirection * _strafeSpeed;
        moveDirection *=  Time.deltaTime;

        Vector3 gravity = Physics.gravity * Time.deltaTime;

        if (_characterController.isGrounded)
        {
            _characterController.Move(moveDirection);
        }
        else
        {
            _characterController.Move(moveDirection + gravity);
        }
    }
}