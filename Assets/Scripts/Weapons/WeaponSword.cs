using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class WeaponSword : MonoBehaviour
{
    public float damage = 2f;
    public float attackSpeed = 1f;

    public float debugAngle = 0f;

    public bool isAttacking = false;

    public Animator animator;

    public void Attack(bool value)
    {
        isAttacking = value;
        animator.SetBool("Attack", value);
    }

    public void AttackPredict(bool value, float delayTime)
    {
        isAttacking = value;
        animator.SetBool("Attack", value);
        if (value)
        {
            animator.Play("SwordAttack", -1, delayTime / animator.GetCurrentAnimatorClipInfo(0).Length);
        }
    }

    public void OnCollision(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<IDamageable>() != null)
        {
            collision.gameObject.GetComponent<IDamageable>().Takedamage(gameObject, damage);
        }
    }
}
