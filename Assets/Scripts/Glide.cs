using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[System.Obsolete("Not working as expected for intended purpose")]
public class Glide : MonoBehaviour
{
    [SerializeField] private float _repellForce = 0.0f;

    Rigidbody2D _rb;

    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        ContactPoint2D[] contacts = null;
        other.GetContacts(contacts);
        
        foreach (var contact in contacts)
        {
            _rb.AddForce(((Vector2) transform.position - contact.point).normalized * _repellForce);
        }
    }
}
