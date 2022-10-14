using UnityEngine;

public class DamageOnImpact : MonoBehaviour
{
    [SerializeField] private int _damageOnImpact = 0;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<Mortality>(out Mortality otherMortality)) {
            otherMortality.damage(_damageOnImpact);
        }
    }
}
