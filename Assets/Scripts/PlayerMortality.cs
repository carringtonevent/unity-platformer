using UnityEngine;
using TMPro;

public class PlayerMortality : Mortality {
    [SerializeField] private Transform _spawnpoint;

    [SerializeField] private int _startHealth = 5;

    [SerializeField] private float _damageJumpForce = 0f;

    private Rigidbody2D _rb;

    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
        onHealthChange += _onHealthChangeListener;
    }

    private void Start() {
        Health = _startHealth;
    }

    private void OnDestroy() {
        onHealthChange -= _onHealthChangeListener;
    }

    private void _onHealthChangeListener(double health) {

    }

    protected override int onDamage(int amount) {
        if(amount < Health) {
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

        Health = _startHealth;
    }
}
