using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour, IDamageable
{
    public Health health;

    public void Takedamage(float value, GameObject gameObject)
    {
        health.TakeDamage(value);
    }
}
