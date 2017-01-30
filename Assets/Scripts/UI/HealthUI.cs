using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{

    public Image healthIndecator;
    public Image expIndecator;

    void Awake()
    {
        if (The.Player != null)
        {
            The.Player.Health.OnPlayerHealthChange += HealthChanged;
        }
    }


    private void HealthChanged()
    {
        healthIndecator.fillAmount = The.Player.Health.Health * GetHealthProportion();
    }

    private float GetHealthProportion()
    {
        return The.Player.Health.GetTotalHealth()/10000;
    }

}
