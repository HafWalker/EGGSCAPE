using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthImage;

    public void UpdateHealtBarView(float currentHealth, float maxHealt)
    {
        healthImage.fillAmount = currentHealth / maxHealt;
    }
}