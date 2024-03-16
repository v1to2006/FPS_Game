using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private readonly string ButtonJump = "Jump";

    private readonly string AxisHorizontal = "Horizontal";
    private readonly string AxisVertical = "Vertical";

    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpDelay;
    [SerializeField] private float _gravityForce;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundDistance;
    [SerializeField] private LayerMask _groundLayer;

    private CharacterController _characterController;
    private Vector3 _velocity;
    private float _startVelocityY = -5f;
    private bool _isGrounded;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        _isGrounded = IsGrounded();

        if (_isGrounded && _velocity.y < 0f)
        {
            _velocity.y = _startVelocityY;
        }

        Move();
        Jump();
        Fall();
    }

    private void Move()
    {
        float horizontal = Input.GetAxisRaw(AxisHorizontal);
        float vertical = Input.GetAxisRaw(AxisVertical);

        Vector3 direction = transform.right * horizontal + transform.forward * vertical;

        _characterController.Move(direction.normalized * _movementSpeed * Time.deltaTime);
    }

    private void Jump()
    {
        if (Input.GetButtonDown(ButtonJump) && _isGrounded)
        {
            _velocity.y = Mathf.Sqrt(_jumpForce * -2f * _gravityForce);
        }
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundLayer);
    }

    private void Fall()
    {
        _velocity.y += _gravityForce * Time.deltaTime;

        _characterController.Move(_velocity * Time.deltaTime);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(_groundCheck.position, _groundDistance);
    }
}
