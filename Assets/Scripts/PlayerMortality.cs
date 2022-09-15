using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMortality : Mortality
{
    [SerializeField] private Transform _spawnpoint;

    [SerializeField] private float _startHealth = 10f;

    [SerializeField] private float _damageJumpForce = 0f; 

    private Rigidbody2D _rb;

    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        _health = _startHealth;
    }

    protected override float onDamage(float amount) {
        if(amount < _health) {
            _rb.AddForce(Quaternion.AngleAxis(Random.Range(-70f, 70f), Vector3.forward) * Vector2.up * _damageJumpForce);
        }

        return amount;
    }

    protected override void death()
    {
        if(_spawnpoint != null) {
            transform.position = _spawnpoint.position;
        }

        _rb.velocity = Vector2.zero;
        
        _health = _startHealth;
    }
}
