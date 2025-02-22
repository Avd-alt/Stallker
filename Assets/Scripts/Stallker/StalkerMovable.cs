using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class StalkerMovable : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _speed = 4f;
    [SerializeField] private float _stoppingDistance = 3f;
    [SerializeField] private float _rotationSpeed;

    private Rigidbody _rigidbody;
    private Vector3 _directionToPlayer;
    private Vector3 _surfaceNormal;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();
        RotateToTarget(_directionToPlayer);
    }

    private void Move()
    {
        float multiplier = 10f;

        if (IsWithinRange(_player.transform.position))
        {
            _rigidbody.velocity = new Vector3(0f, _rigidbody.velocity.y, 0f);
        }
        else
        {
            _surfaceNormal = GetNormalSurface();
            _directionToPlayer = (_player.transform.position - transform.position).normalized;

            Vector3 projectedDirection = Vector3.ProjectOnPlane(_directionToPlayer, _surfaceNormal).normalized;
            Vector3 targetVelocity = new Vector3(projectedDirection.x * _speed, _rigidbody.velocity.y, projectedDirection.z * _speed);

            _rigidbody.AddForce((targetVelocity - _rigidbody.velocity) * multiplier, ForceMode.Acceleration);
        }
    }

    private void RotateToTarget(Vector3 target)
    {
        if (_directionToPlayer != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_directionToPlayer, _surfaceNormal);

            _rigidbody.MoveRotation(Quaternion.Slerp(_rigidbody.rotation, targetRotation, _rotationSpeed * Time.fixedDeltaTime));
        }
    }

    private Vector3 GetNormalSurface()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 2f))
        {
            return hit.normal;
        }

        return Vector3.zero;
    }


    private bool IsWithinRange(Vector3 target)
    {
        int degree = 2;
        float rangeDetectionSqrt = Mathf.Pow(_stoppingDistance, degree);
        float distance = (transform.position - target).sqrMagnitude;

        return distance <= rangeDetectionSqrt;
    }
}