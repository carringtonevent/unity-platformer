using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnImpact : MonoBehaviour
{
    [SerializeField] private float _damageOnImpact = 0f;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.TryGetComponent<Mortality>(out Mortality otherMortality)) {
            otherMortality.damage(_damageOnImpact);
        }       
    }
}
