using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mortality : MonoBehaviour
{
    [SerializeField] protected int _health = 1;

    protected abstract void death();

    protected virtual int onDamage(int amount) => amount;

    protected event Action<double> onHealthChange;

    public void kill() {
        Health = 0;
        
        death();
    }

    public void damage(int amount) {
        Health -= onDamage(amount);

        if(Health <= 0) {
            Health = 0;

            death();
        }
    }

    public void heal(int amount) => Health += amount;

    public int Health {
        get => _health;
        set {
            _health = value;
            onHealthChange?.Invoke(_health);
        }
    }
}