using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSword : Weapon
{
    public Animator animator;

    void Start()
    {
        damage = 2;
    }

    public override void Attack(bool v)
    {
        animator.SetBool("Attack", v);
    }
}
