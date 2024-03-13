using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealt = 10f;
    public float currentHealth;

    private bool _canTakeDamage = true;
    private float _timeToResetHealth = 2f;
    private float _timeCounter = 0f;

    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealt;
    }

    void Update()
    {
        if (!_canTakeDamage)
        {
            _timeCounter += Time.deltaTime;
            if (_timeCounter >= _timeToResetHealth)
            {
                _timeCounter = 0f;
                ResetHealth();
                _canTakeDamage = true;
            }
        }
    }

    public void TakeDamage(float value)
    {
        currentHealth -= value;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            _canTakeDamage = false;
        }

        healthBar.UpdateHealtBarView(currentHealth, maxHealt);
    }

    public void ResetHealth()
    {
        currentHealth = maxHealt;
        healthBar.UpdateHealtBarView(currentHealth, maxHealt);
    }
}