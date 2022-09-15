using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieInVoid : MonoBehaviour
{
    [SerializeField] private float _yOfVoid = -100f;

    private Mortality _mort;

    void Awake() {
        _mort = GetComponent<Mortality>();
    }

    void FixedUpdate()
    {
        if(transform.position.y < _yOfVoid) {
            _mort.kill();
        }
    }
}
