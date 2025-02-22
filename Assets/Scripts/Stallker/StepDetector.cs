using UnityEngine;

public class LadderClimber : MonoBehaviour
{
    [SerializeField] private float _stepHeight = 1f;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _groundCheckDistance = 1f;

    private Rigidbody _rigidbody;
    private bool _isOnLadder = false;

    private void Awake()
    {
        _rigidbody = GetComponentInParent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (_isOnLadder == false && IsGrounded() == false)
        {
            _rigidbody.AddForce(Physics.gravity * _rigidbody.mass, ForceMode.Acceleration);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Ladder ladder))
        {
            _isOnLadder = true;

            if (IsGrounded() == true)
            {
                Vector3 newPosition = transform.position + Vector3.up * _stepHeight;
                _rigidbody.MovePosition(newPosition);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Ladder ladder))
        {
            _isOnLadder = false;
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, _groundCheckDistance, _groundLayer);
    }
}