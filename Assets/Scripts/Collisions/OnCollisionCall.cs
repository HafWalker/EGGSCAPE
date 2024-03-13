using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionCall : MonoBehaviour
{
    public WeaponSword sword;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        sword.OnCollision(collision);
    }
}