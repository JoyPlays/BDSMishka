using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Timers;
using UnityEngine;

public class PlayerHealth
{

    private float baseHealth;
    private float onePercentOfBaseHealth;

    private float totalHealth = 0;
    private float currentHealth;

    private float skillsBonusPercent = 0;
    private float artifactsBonusPercent = 0;

    private Timer restoreTimer;


    public bool IsAlive;


    #region Properties

    public float Health
    {
        get { return currentHealth; }
    }

    public float SkillBonus
    {
        get { return onePercentOfBaseHealth * skillsBonusPercent; }
        set
        {
            skillsBonusPercent = value;
            totalHealth = baseHealth + SkillBonus + ArtifactBonus;
        }
    }

    public float ArtifactBonus
    {
        get { return onePercentOfBaseHealth * artifactsBonusPercent; }
        set
        {
            artifactsBonusPercent = value;
            totalHealth = Convert.ToInt32(baseHealth + SkillBonus + ArtifactBonus);
        }
    }

    #endregion

    #region Constructors

    public PlayerHealth()
    {
        SetBaseHealth(The.settings.PlayerBaseHealth);
        currentHealth = totalHealth;
        restoreTimer = new Timer(The.settings.HealthRestoreTickTime) {AutoReset = true, Enabled = true};
        restoreTimer.Elapsed += Regenerate;
    }

    #endregion

    #region Getters and Setters

    public void SetBaseHealth(float health)
    {
        baseHealth = health;
        onePercentOfBaseHealth = baseHealth / 100;
        totalHealth = Convert.ToInt32(baseHealth + SkillBonus + ArtifactBonus);
    }

    public void IncreaseBaseHealth(float increasePercent)
    {
        baseHealth = Convert.ToInt32(increasePercent * onePercentOfBaseHealth);
        onePercentOfBaseHealth = baseHealth / 100;
        totalHealth = Convert.ToInt32(baseHealth + SkillBonus + ArtifactBonus);
    }

    public float GetTotalHealth()
    {
        return totalHealth;
    }

    #endregion

    #region Functionality

    public void TakeDamage(float damage)
    {
        currentHealth = currentHealth - damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            IsAlive = false;
        }
    }

    public void ResetHealth()
    {
        currentHealth = totalHealth;
    }

    public void AddHealth(float health)
    {
        currentHealth += health;
        if (currentHealth > totalHealth)
        {
            currentHealth = totalHealth;
            StopRegeneration();
        }
    }

    public void Regenerate(object sender, ElapsedEventArgs e)
    {
        AddHealth(The.settings.HealthRestorePerTick);
    }

    public void StartRegeneration()
    {
        restoreTimer.Start();
    }

    public void StopRegeneration()
    {
        restoreTimer.Stop();
    }
    #endregion


}
