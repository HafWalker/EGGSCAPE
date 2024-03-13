using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public virtual float damage { get; set; }
    public virtual void Attack(bool value) { }

    public void OnCollision(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<IDamageable>() != null)
        {
            collision.gameObject.GetComponent<IDamageable>().Takedamage(damage, gameObject);
        }
    }
}