using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float _force = 10;
    [SerializeField] private float _maxSpeed = 20;
    [SerializeField] private float _jumpForce = 20;

    [SerializeField] private Transform _foot1Transform;
    [SerializeField] private Transform _foot2Transform;

    private Rigidbody2D _rb;

    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {

    }

    private void FixedUpdate() {
        _setVelocityInput();
        _setJumpInput();
    }

    private void _setVelocityInput() {
        _rb.AddForce(new Vector2(Input.GetAxisRaw("Horizontal") * _force, 0));

        if (Mathf.Abs(_rb.velocity.x) > _maxSpeed) {
            _rb.velocity = new Vector2((_rb.velocity.x < 0 ? -1 : 1) * _maxSpeed, _rb.velocity.y);
        }
    }

    private void _setJumpInput() {
        if (Input.GetButton("Jump") && _isGrounded()) {
            _rb.AddForce(new Vector2(0, _jumpForce));
        }
    }

    private bool _isGrounded() {
        var directionVector = _foot2Transform.position - _foot1Transform.position;
        var hitList = Physics2D.RaycastAll(_foot1Transform.position, directionVector, directionVector.magnitude);
        // TODO Filter Player out
        return hitList.Length > 0;
    }
}
