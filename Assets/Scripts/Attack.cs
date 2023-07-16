using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private bool _canDamage = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hit " + collision.name);

        IDamageable hit = collision.GetComponent<IDamageable>();
        if(hit != null)
        {
            if(_canDamage == true)
            {
                hit.Damage();
                _canDamage = false;
                StartCoroutine(ResetDamage());
            }
            
        }
    }

    IEnumerator ResetDamage()
    {
        yield return new WaitForSeconds(1.0f);
        _canDamage = true;
    }
}
