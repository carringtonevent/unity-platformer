using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mortality : MonoBehaviour
{
    [SerializeField] protected float _health = 1f;

    protected abstract void death();

    protected virtual float onDamage(float amount) => amount;

    public void kill() {
        _health = 0;
        
        death();
    }

    public void damage(float amount) {
        _health -= onDamage(amount);

        if(_health <= 0) {
            _health = 0;

            death();
        }
    }

    public void heal(float amount) => _health += amount;

    public float Health { get => _health; set => _health = value; }
    
}