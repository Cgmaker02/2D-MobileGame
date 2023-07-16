using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy,IDamageable
{
    public int Health { get; set; }

    public override void Init()
    {
        base.Init();
        Health = base.health;
    }
    public void Damage()
    {
        if (isDead == true)
            return;

        health -= 1;
        anim.SetTrigger("Hit");
        isHit = true;
        anim.SetBool("inCombat", true);

        if(health < 1)
        {
            isDead = true;
            anim.SetTrigger("Death");
            GameObject diamond = Instantiate(diamondPrefab, transform.position, Quaternion.identity) as GameObject;
            diamond.GetComponent<Diamond>().gems = base.gems;
        }
    }
   
}
