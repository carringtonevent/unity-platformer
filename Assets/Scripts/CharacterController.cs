using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [Header("Configure")]
    [SerializeField] private float _force = 10f;
    [SerializeField] private float _maxSpeed = 20f;
    [SerializeField] private float _jumpForce = 20f;
    [SerializeField] private float _jumpContinuationForce = 0f;

    [SerializeField] private float _maxJumpDuration = 0f;
    [SerializeField] private float _linearDragAir = 2f;
    private float _velocityMultiplierGround = 2;

    [SerializeField] private Transform _foot1Transform;
    [SerializeField] private Transform _foot2Transform;

    private Rigidbody2D _rb;

    private bool _jumpTriggered = false;

    private float _jumpStartTime = 0;

    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {

    }

    private void FixedUpdate() {
        _setVelocityInput();
        _setJumpInput();
        _setLinearDrag();
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_foot1Transform.position, _foot2Transform.position);
    }

    private void _setVelocityInput() {
        _rb.AddForce(new Vector2(Input.GetAxisRaw("Horizontal") * _force * (_isGrounded() ? _velocityMultiplierGround : 1), 0));

        if (Mathf.Abs(_rb.velocity.x) > _maxSpeed) {
            _rb.velocity = new Vector2((_rb.velocity.x < 0 ? -1 : 1) * _maxSpeed, _rb.velocity.y);
        }
    }

    private float _secondsSinceStartJump() => Time.fixedTime - _jumpStartTime;

    private void _setJumpInput() {
        if (Input.GetButton("Jump") && _isGrounded() && ! _jumpTriggered) {
            _rb.velocity = new Vector2(_rb.velocity.x, 0);
            _rb.AddForce(new Vector2(0, _jumpForce));
            
            _jumpTriggered = true;

            _jumpStartTime = Time.fixedTime;
        }

        if (! Input.GetButton("Jump")) {
            _jumpTriggered = false;
        }

        float secondsSinceStartJump = _secondsSinceStartJump();

        if (Input.GetButton("Jump") && secondsSinceStartJump <= _maxJumpDuration && _rb.velocity.y > 0) {
            _rb.AddForce(new Vector2(0, Mathf.Lerp(_jumpContinuationForce, 0, secondsSinceStartJump / _maxJumpDuration)));
        }
    }

    private void _setLinearDrag() {
        if (_isGrounded()) {
            //_rb.drag = _linearDragGround;
            _rb.drag = _linearDragAir * _velocityMultiplierGround * (Input.GetAxisRaw("Horizontal") == 0 ? 2 : 1);
        } else {
            _rb.drag = _linearDragAir;
        }
    }

    private bool _isGrounded() {
        var directionVector = _foot2Transform.position - _foot1Transform.position;
        var hitList = Physics2D.RaycastAll(_foot1Transform.position, directionVector, directionVector.magnitude);
        // TODO Filter Player out
        return hitList.Length > 0;
    }
}
