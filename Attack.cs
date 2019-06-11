using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private bool _canDamage = true;

    private void OnTriggerEnter2D(Collider2D other)
    {

        IDamageable hit = other.GetComponent<IDamageable>();

        if (hit != null)
        {
            Debug.Log("Hit: " + other.name);
            if (_canDamage)
            {
                hit.Damage();
                _canDamage = false;
                StartCoroutine(ResetDamageRoutine());
            }
        }
    }

    IEnumerator ResetDamageRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        _canDamage = true;
    }
}
