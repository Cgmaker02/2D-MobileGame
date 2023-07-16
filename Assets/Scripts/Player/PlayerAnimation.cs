using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _anim;
    private Animator _animArc;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        _animArc = transform.GetChild(1).GetComponent<Animator>();
    }

    // Update is called once per frame
    public void PlayerMove(float move)
    {
        _anim.SetFloat("Run", Mathf.Abs(move));
    }

    public void Jump(bool jumping)
    {
        _anim.SetBool("Jumping", jumping);
    }

    public void Attack()
    {
        _anim.SetTrigger("Attack");
        _animArc.SetTrigger("Arc");
    }

    public void Death()
    {
        _anim.SetTrigger("Death");
    }
}
