using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionCall : MonoBehaviour
{
    public Weapon weapon;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        weapon.OnCollision(collision);
    }
}